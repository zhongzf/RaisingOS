using System;
using System.Runtime;
using Uefi;
using BOOT;

unsafe class Program
{
  public const int CommandMaxLength = 100;

  protected static EFI_SYSTEM_TABLE* _systemTable;
  protected static EFI_SIMPLE_POINTER_PROTOCOL* _sop;
  protected static EFI_GRAPHICS_OUTPUT_PROTOCOL* _gop;

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
    _systemTable->ConOut->ClearScreen(systemTable->ConOut);
    _systemTable->BootServices->SetWatchdogTimer(0, 0, 0, null);

    fixed (EFI_SIMPLE_POINTER_PROTOCOL** p = &_sop)
    {
      _systemTable->BootServices->LocateProtocol((EFI_GUID*)EFI.EFI_SIMPLE_POINTER_PROTOCOL_GUID, null, (void**)p);
    }

    fixed (EFI_GRAPHICS_OUTPUT_PROTOCOL** p = &_gop)
    {
      _systemTable->BootServices->LocateProtocol((EFI_GUID*)EFI.EFI_GRAPHICS_OUTPUT_PROTOCOL_GUID, null, (void**)p);
    }
  }

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
    int x0 = (int)p1.x;
    int y0 = (int)p1.y;
    int x1 = (int)p2.x;
    int y1 = (int)p2.y;
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
    int x = (int)rect.point.x;
    int y = (int)rect.point.y;
    int width = (int)rect.size.width;
    int height = (int)rect.size.height;
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

    _sop->Mode->ResolutionX = width;
    _sop->Mode->ResolutionY = height;
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
      _sop->GetState(_sop, &sts);

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
      else
      {
        OutputString("Command not found.\r\n");
      }
    }
  }
}
