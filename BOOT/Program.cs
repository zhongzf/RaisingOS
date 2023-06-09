﻿using System;
using System.Runtime;
using Uefi;
using BOOT.Graphics;
using BOOT.FileSystem;
using Internal.Runtime.CompilerHelpers;

unsafe class Program
{
  public const int CommandMaxLength = 100;
  public const int MaxBufferSize = 1024;
  public const int MaxFileNumber = 1024;

  public const int FileRectangleWidth = 64;
  public const int FileRectangleHeight = 64;

  protected static EFI_SYSTEM_TABLE* _systemTable;
  protected static EFI_SIMPLE_POINTER_PROTOCOL* _spp;
  protected static EFI_GRAPHICS_OUTPUT_PROTOCOL* _gop;
  protected static EFI_SIMPLE_FILE_SYSTEM_PROTOCOL* _sfsp;

  static void Main() { }

  [RuntimeExport("EfiMain")]
  static EFI_STATUS EfiMain(EFI_HANDLE imageHandle, EFI_SYSTEM_TABLE* systemTable)
  {
    EfiInit(systemTable);

    Shell();

    return (EFI_STATUS)(RETURN_STATUS)0;
  }

  private static void EfiInit(EFI_SYSTEM_TABLE* systemTable)
  {
    _systemTable = systemTable;
    StartupCodeHelpers.SystemTable = _systemTable;

    _systemTable->ConOut->ClearScreen(systemTable->ConOut);
    _systemTable->BootServices->SetWatchdogTimer(0, 0, 0, null);

    fixed (EFI_SIMPLE_POINTER_PROTOCOL** p = &_spp)
    {
      _systemTable->BootServices->LocateProtocol((EFI_GUID*)EFI.EFI_SIMPLE_POINTER_PROTOCOL_GUID, null, (void**)p);
    }

    fixed (EFI_GRAPHICS_OUTPUT_PROTOCOL** p = &_gop)
    {
      _systemTable->BootServices->LocateProtocol((EFI_GUID*)EFI.EFI_GRAPHICS_OUTPUT_PROTOCOL_GUID, null, (void**)p);
    }

    fixed (EFI_SIMPLE_FILE_SYSTEM_PROTOCOL** p = &_sfsp)
    {
      _systemTable->BootServices->LocateProtocol((EFI_GUID*)EFI.EFI_SIMPLE_FILE_SYSTEM_PROTOCOL_GUID, null, (void**)p);
    }
  }

  #region Shell
  public static void OutputChar(char c)
  {
    var s = stackalloc char[2];
    s[0] = c;
    _systemTable->ConOut->OutputString(_systemTable->ConOut, s);
  }

  public static void OutputString(string s)
  {
    fixed (char* p = s)
    {
      _systemTable->ConOut->OutputString(_systemTable->ConOut, p);
    }
  }

  public static void OutputString(char* s)
  {
    _systemTable->ConOut->OutputString(_systemTable->ConOut, s);
  }

  public static char GetChar()
  {
    EFI_INPUT_KEY key;
    ulong index;

    _systemTable->BootServices->WaitForEvent(1, &(_systemTable->ConIn->WaitForKey), &index);
    while ((RETURN_STATUS)(EFI_STATUS)_systemTable->ConIn->ReadKeyStroke(_systemTable->ConIn, &key) > 0)
      ;

    return key.UnicodeChar;
  }

  public static int GetString(char* buf, int bufSize)
  {
    var i = 0;
    for (; i < bufSize - 1;)
    {
      buf[i] = GetChar();
      OutputChar(buf[i]);
      if (buf[i] == '\b')
      {
        i--;
        continue;
      }
      else if (buf[i] == '\r')
      {
        OutputChar('\n');
        break;
      }
      i++;
    }
    buf[i] = '\0';
    return i;
  }

  public static bool StringCompare(string s1, char* s2)
  {
    if (s2[s1.Length] != '\0')
    {
      return false;
    }
    fixed (char* p1 = s1)
    {
      for (int i = 0; i < s1.Length; i++)
      {
        if (p1[i] != s2[i])
        {
          return false;
        }
      }
    }
    return true;
  }

  public static void StringCopy(char* s1, char* s2, int length)
  {
    for (int i = 0; i < length; i++)
    {
      s2[i] = s1[i];
    }
  }
  #endregion

  #region GUI
  public static void DrawCursor(uint* fb, int* cursor, int x, int y)
  {
    for (int h = 0; h < 19; h++)
    {
      for (int w = 0; w < 12; w++)
      {
        if (cursor[h * 12 + w] == 1)
        {
          SetPixel(fb, w + x, h + y, 0);
        }
        if (cursor[h * 12 + w] == 2)
        {
          SetPixel(fb, w + x, h + y, 0xFFFFFFFF);
        }
      }
    }
  }

  public static void SetPixel(uint* fb, int x, int y, uint color)
  {
    var width = _gop->Mode->Info->HorizontalResolution;
    fb[width * y + x] = color;
  }

  public static uint GetPixel(uint* fb, int x, int y)
  {
    var width = _gop->Mode->Info->HorizontalResolution;
    return fb[width * y + x];
  }

  public static void DrawLine(uint* fb, Point p1, Point p2, uint color)
  {
    int x0 = (int)p1.X;
    int y0 = (int)p1.Y;
    int x1 = (int)p2.X;
    int y1 = (int)p2.Y;
    DrawLine(fb, x0, y0, x1, y1, color);
  }

  public static void DrawLine(uint* fb, int x0, int y0, int x1, int y1, uint color)
  {
    int dx = (x0 < x1) ? (x1 - x0) : (x0 - x1);
    int sx = (x0 < x1) ? 1 : -1;
    int dy = (y0 < y1) ? (y1 - y0) : (y0 - y1);
    int sy = (y0 < y1) ? 1 : -1;
    int e2, err = dx - dy;
    while (true)
    {
      SetPixel(fb, x0, y0, color);

      if ((x0 == x1) && (y0 == y1)) return;

      e2 = 2 * err;
      if (e2 > -dy)
      {
        err -= dy;
        x0 += sx;
      }
      if (e2 < dx)
      {
        err += dx;
        y0 += sy;
      }
    }
  }

  public static void DrawRectangle(uint* fb, Rectangle rect, uint color)
  {
    int x = (int)rect.Point.X;
    int y = (int)rect.Point.Y;
    int width = (int)rect.Size.Width;
    int height = (int)rect.Size.Height;
    DrawRectangle(fb, x, y, width, height, color);
  }

  public static void DrawRectangle(uint* fb, int x, int y, int width, int height, uint color)
  {
    DrawLine(fb, x, y, x + width, y, color);
    DrawLine(fb, x + width, y, x + width, y + height, color);
    DrawLine(fb, x + width, y + height, x, y + height, color);
    DrawLine(fb, x, y + height, x, y, color);
  }

  public static int Clamp(int value, int min, int max)
  {
    if (value < min) return min;
    if (value > max) return max;
    return value;
  }

  public static void GetFrameBuffer(out uint* fb, out uint width, out uint height)
  {
    fb = (uint*)(void*)(ulong)_gop->Mode->FrameBufferBase;

    width = _gop->Mode->Info->HorizontalResolution;
    height = _gop->Mode->Info->VerticalResolution;

    _spp->Mode->ResolutionX = width;
    _spp->Mode->ResolutionY = height;
  }

  public static bool IsInRectangle(int x, int y, Rectangle r)
  {
    return ((r.Point.X <= x) && (x <= (r.Point.X + r.Size.Width - 1)) && (r.Point.Y <= y) && (y <= (r.Point.Y + r.Size.Height - 1)));
  }

  #endregion

  public static int LsGUI()
  {
    var fileList = new File[MaxFileNumber];
    var fileBuffer = stackalloc char[MaxBufferSize];

    EFI_FILE_PROTOCOL* root;
    ulong bufferSize = 0;
    EFI_FILE_INFO* fileInfo;

    GetFrameBuffer(out var fb, out var width, out var height);

    var i = 0;
    var status = _sfsp->OpenVolume(_sfsp, &root);
    while (true)
    {
      status = root->Read(root, &bufferSize, (void*)fileBuffer);
      if (bufferSize == 0) break;

      fileInfo = (EFI_FILE_INFO*)fileBuffer;
      fixed (char* pName = fileList[i].Name)
      {
        StringCopy(fileInfo->FileName, pName, (int)bufferSize);
        fileList[i].Name[bufferSize] = '\0';
      }

      fileList[i].Rectangle.Point.X = (uint)(i * FileRectangleWidth);
      fileList[i].Rectangle.Point.Y = 0;
      fileList[i].Rectangle.Size.Width = FileRectangleWidth;
      fileList[i].Rectangle.Size.Height = FileRectangleHeight;

      DrawRectangle(fb, fileList[i].Rectangle, 0xFFFFFFFF);

      i++;
    }
    root->Close(root);

    #region Cursor
    #pragma warning disable format
    var cursor = stackalloc int[]
    {
            1,0,0,0,0,0,0,0,0,0,0,0,
            1,1,0,0,0,0,0,0,0,0,0,0,
            1,2,1,0,0,0,0,0,0,0,0,0,
            1,2,2,1,0,0,0,0,0,0,0,0,
            1,2,2,2,1,0,0,0,0,0,0,0,
            1,2,2,2,2,1,0,0,0,0,0,0,
            1,2,2,2,2,2,1,0,0,0,0,0,
            1,2,2,2,2,2,2,1,0,0,0,0,
            1,2,2,2,2,2,2,2,1,0,0,0,
            1,2,2,2,2,2,2,2,2,1,0,0,
            1,2,2,2,2,2,2,2,2,2,1,0,
            1,2,2,2,2,2,2,2,2,2,2,1,
            1,2,2,2,2,2,2,1,1,1,1,1,
            1,2,2,2,1,2,2,1,0,0,0,0,
            1,2,2,1,0,1,2,2,1,0,0,0,
            1,2,1,0,0,1,2,2,1,0,0,0,
            1,1,0,0,0,0,1,2,2,1,0,0,
            0,0,0,0,0,0,1,2,2,1,0,0,
            0,0,0,0,0,0,0,1,1,0,0,0
    };
    #pragma warning disable format
    #endregion


    int fileNumber = i;

    ulong index;
    EFI_SIMPLE_POINTER_STATE sts;
    int cursorX = 0;
    int cursorY = 0;
    float MouseSpeed = 200;

    _spp->Reset(_spp, false);
    while (true)
    {
      _systemTable->BootServices->WaitForEvent(1, &(_spp->WaitForInput), &index);
      _spp->GetState(_spp, &sts);

      for (i = 0; i < fileNumber; i++)
      {
        cursorX = Clamp(cursorX + (int)((sts.RelativeMovementX / 65536f) * MouseSpeed), 0, (int)width);
        cursorY = Clamp(cursorY + (int)((sts.RelativeMovementY / 65536f) * MouseSpeed), 0, (int)height);

        //DrawCursor(fb, cursor, cursorX, cursorY);

        if (IsInRectangle(cursorX, cursorY, fileList[i].Rectangle))
        {
          if (!fileList[i].IsHighlighted)
          {
            DrawRectangle(fb, fileList[i].Rectangle, 0xFFFFFF00);
            fileList[i].IsHighlighted = true;
          }
        }
        else
        {
          if (fileList[i].IsHighlighted)
          {
            DrawRectangle(fb, fileList[i].Rectangle, 0xFFFFFFFF);
            fileList[i].IsHighlighted = false;
          }
        }
      }
    }

    return fileNumber;
  }

  public static int Ls()
  {
    var fileList = new File[MaxFileNumber];
    //File file = default;
    var fileBuffer = stackalloc char[MaxBufferSize];
    EFI_FILE_PROTOCOL* root;
    ulong bufferSize = 0;
    EFI_FILE_INFO* fileInfo;

    var i = 0;
    var status = _sfsp->OpenVolume(_sfsp, &root);
    while (true)
    {
      status = root->Read(root, &bufferSize, (void*)fileBuffer);
      if (bufferSize == 0) break;

      fileInfo = (EFI_FILE_INFO*)fileBuffer;
      fixed (char* pName = fileList[i].Name)
      {
        StringCopy(fileInfo->FileName, pName, (int)bufferSize);
        fileList[i].Name[bufferSize] = '\0';
        OutputString(pName);
      }

      //StringCopy(fileInfo->FileName, file.Name, (int)bufferSize);
      //file.Name[bufferSize] = '\0';
      //OutputString(file.Name);

      //OutputString(fileInfo->FileName);
      OutputString("\r\n");
      i++;
    }
    root->Close(root);
    return i;
  }

  private static void GUI()
  {
    #region Cursor
    #pragma warning disable format
    var cursor = stackalloc int[]
    {
            1,0,0,0,0,0,0,0,0,0,0,0,
            1,1,0,0,0,0,0,0,0,0,0,0,
            1,2,1,0,0,0,0,0,0,0,0,0,
            1,2,2,1,0,0,0,0,0,0,0,0,
            1,2,2,2,1,0,0,0,0,0,0,0,
            1,2,2,2,2,1,0,0,0,0,0,0,
            1,2,2,2,2,2,1,0,0,0,0,0,
            1,2,2,2,2,2,2,1,0,0,0,0,
            1,2,2,2,2,2,2,2,1,0,0,0,
            1,2,2,2,2,2,2,2,2,1,0,0,
            1,2,2,2,2,2,2,2,2,2,1,0,
            1,2,2,2,2,2,2,2,2,2,2,1,
            1,2,2,2,2,2,2,1,1,1,1,1,
            1,2,2,2,1,2,2,1,0,0,0,0,
            1,2,2,1,0,1,2,2,1,0,0,0,
            1,2,1,0,0,1,2,2,1,0,0,0,
            1,1,0,0,0,0,1,2,2,1,0,0,
            0,0,0,0,0,0,1,2,2,1,0,0,
            0,0,0,0,0,0,0,1,1,0,0,0
    };
    #pragma warning disable format
    #endregion

    GetFrameBuffer(out var fb, out var width, out var height);

    EFI_SIMPLE_POINTER_STATE sts;
    float MouseSpeed = 200;

    int cursorX = 0;
    int cursorY = 0;

    for (; ; )
    {
      _spp->GetState(_spp, &sts);

      cursorX = Clamp(cursorX + (int)((sts.RelativeMovementX / 65536f) * MouseSpeed), 0, (int)width);
      cursorY = Clamp(cursorY + (int)((sts.RelativeMovementY / 65536f) * MouseSpeed), 0, (int)height);

      DrawCursor(fb, cursor, cursorX, cursorY);
    }
  }

  public static void Rect()
  {
    GetFrameBuffer(out var fb, out var width, out var height);

    DrawRectangle(fb, 10, 10, 100, 100, 0xFF0000FF);
  }

  public static void Shell()
  {
    var command = stackalloc char[CommandMaxLength];
    while (true)
    {
      OutputString("RaisingOS > ");

      if (GetString(command, CommandMaxLength) <= 0)
        continue;

      if (StringCompare("hello", command))
      {
        OutputString("Hello UEFI!\r\n");
      }
      else if (StringCompare("gui", command))
      {
        GUI();
      }
      else if(StringCompare("rect", command))
      {
        Rect();
      }
      else if(StringCompare("ls", command))
      {
        Ls();
      }
      else if (StringCompare("lsgui", command))
      {
        LsGUI();
      }
      else
      {
        OutputString("Command not found.\r\n");
      }
    }
  }
}
