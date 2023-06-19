using System;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#region A couple very basic things
namespace System
{
  public class Object
  {
#pragma warning disable 169
    // The layout of object is a contract with the compiler.
    internal unsafe MethodTable* m_pMethodTable;
#pragma warning restore 169
  }
  public struct Void { }

  // The layout of primitive types is special cased because it would be recursive.
  // These really don't need any fields to work.
  public struct Boolean { }
  public struct Char { }
  public struct SByte { }
  public struct Byte { }
  public struct Int16 { }
  public struct UInt16 { }
  public struct Int32 { }
  public struct UInt32 { }
  public struct Int64 { }
  public struct UInt64 { }
  public struct IntPtr { }
  public struct UIntPtr { }
  public struct Single { }
  public struct Double { }

  public abstract class ValueType { }
  public abstract class Enum : ValueType { }

  public struct Nullable<T> where T : struct { }

  public sealed class String { public readonly int Length; }

  public abstract class Array
  {
    private readonly int _length;

    public int Length => _length;
  }

  public abstract class Delegate { }
  public abstract class MulticastDelegate : Delegate { }

  public struct RuntimeTypeHandle { }
  public struct RuntimeMethodHandle { }
  public struct RuntimeFieldHandle { }

  public class Attribute { }

  public enum AttributeTargets { }

  public sealed class AttributeUsageAttribute : Attribute
  {
    public AttributeUsageAttribute(AttributeTargets validOn) { }
    public bool AllowMultiple { get; set; }
    public bool Inherited { get; set; }
  }

  public class AppContext
  {
    public static void SetData(string s, object o) { }
  }

  namespace Runtime.CompilerServices
  {
    public class RuntimeHelpers
    {
      public static unsafe int OffsetToStringData => sizeof(IntPtr) + sizeof(int);
    }

    public static class RuntimeFeature
    {
      public const string UnmanagedSignatureCallingConvention = nameof(UnmanagedSignatureCallingConvention);
    }

    public static class IsVolatile
    {
    }
  }
}

namespace System.Runtime.InteropServices
{
  public class UnmanagedType { }

  sealed class StructLayoutAttribute : Attribute
  {
    public StructLayoutAttribute(LayoutKind layoutKind)
    {
    }
  }

  internal enum LayoutKind
  {
    Sequential = 0, // 0x00000008,
    Explicit = 2, // 0x00000010,
    Auto = 3, // 0x00000000,
  }

  internal enum CharSet
  {
    None = 1,       // User didn't specify how to marshal strings.
    Ansi = 2,       // Strings should be marshalled as ANSI 1 byte chars.
    Unicode = 3,    // Strings should be marshalled as Unicode 2 byte chars.
    Auto = 4,       // Marshal Strings in the right way for the target system.
  }

  public sealed class FieldOffsetAttribute : Attribute
  {
    public FieldOffsetAttribute(int offset)
    {
      Value = offset;
    }

    public int Value { get; }
  }
}
#endregion

namespace System
{
  public class Type { }
  public class RuntimeType : Type { }

  //public readonly ref struct ReadOnlySpan<T>
  //{
  //  private readonly ref T _reference;
  //  private readonly int _length;

  //  public ReadOnlySpan(T[] array)
  //  {
  //    if (array == null)
  //    {
  //      this = default;
  //      return;
  //    }

  //    _reference = ref MemoryMarshal.GetArrayDataReference(array);
  //    _length = array.Length;
  //  }

  //  public unsafe ReadOnlySpan(void* pointer, int length)
  //  {
  //    _reference = ref Unsafe.As<byte, T>(ref *(byte*)pointer);
  //    _length = length;
  //  }

  //  public ref readonly T this[int index]
  //  {
  //    [Intrinsic]
  //    get
  //    {
  //      if ((uint)index >= (uint)_length)
  //        // TODO: FailFast(null);
  //        while (true) ;
  //      return ref Unsafe.Add(ref _reference, (nint)(uint)index);
  //    }
  //  }

  //  public static implicit operator ReadOnlySpan<T>(T[] array) => new ReadOnlySpan<T>(array);
  //}
}

namespace System.Runtime.InteropServices
{
  public sealed class InAttribute : Attribute
  {
  }

  public sealed class OutAttribute : Attribute
  {
  }

  public static class MemoryMarshal
  {
    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T GetArrayDataReference<T>(T[] array) => ref Unsafe.As<byte, T>(ref Unsafe.As<RawArrayData>(array).Data);
  }
}

namespace System.Runtime.CompilerServices
{
  internal sealed class IntrinsicAttribute : Attribute { }

  public enum MethodImplOptions
  {
    Unmanaged = 0x0004,
    NoInlining = 0x0008,
    ForwardRef = 0x0010,
    Synchronized = 0x0020,
    NoOptimization = 0x0040,
    PreserveSig = 0x0080,
    AggressiveInlining = 0x0100,
    AggressiveOptimization = 0x0200,
    InternalCall = 0x1000
  }

  public sealed class MethodImplAttribute : Attribute
  {
    public MethodImplAttribute(MethodImplOptions methodImplOptions) { }
  }

  public sealed class IndexerNameAttribute : Attribute
  {
    public IndexerNameAttribute(string indexerName) { }
  }

  [StructLayout(LayoutKind.Sequential)]
  internal class RawArrayData
  {
    public uint Length; // Array._numComponents padded to IntPtr
    public uint Padding;
    public byte Data;
  }

  public static unsafe partial class Unsafe
  {
    // The body of this method is generated by the compiler.
    // It will do what Unsafe.Add is expected to do. It's just not possible to express it in C#.
    [Intrinsic]
    public static extern ref T Add<T>(ref T source, int elementOffset);
    [Intrinsic]
    public static extern ref T Add<T>(ref T source, IntPtr elementOffset);
    [Intrinsic]
    public static extern ref TTo As<TFrom, TTo>(ref TFrom source);
    [Intrinsic]
    public static extern T As<T>(object o) where T : class;
  }
}

namespace Internal.Runtime.CompilerHelpers
{
  class ThrowHelpers
  {
    static void ThrowIndexOutOfRangeException() { }
    static void ThrowDivideByZeroException() { }
  }
}

#region Things needed by ILC
namespace System
{
  namespace Runtime
  {
    internal sealed class RuntimeExportAttribute : Attribute
    {
      public RuntimeExportAttribute(string entry) { }
    }

    internal sealed class RuntimeImportAttribute : Attribute
    {
      public RuntimeImportAttribute(string lib) { }
      public RuntimeImportAttribute(string lib, string entry) { }
    }

    internal unsafe struct MethodTable
    {
      internal ushort _usComponentSize;
      private ushort _usFlags;
      internal uint _uBaseSize;
      internal MethodTable* _relatedType;
      private ushort _usNumVtableSlots;
      private ushort _usNumInterfaces;
      private uint _uHashCode;
    }
  }

  class Array<T> : Array { }
}

namespace Internal.Runtime.CompilerHelpers
{
  using System.Runtime;
  using System.Runtime.CompilerServices;
  using Uefi;

  // A class that the compiler looks for that has helpers to initialize the
  // process. The compiler can gracefully handle the helpers not being present,
  // but the class itself being absent is unhandled. Let's add an empty class.
  class StartupCodeHelpers
  {
    public unsafe static EFI_SYSTEM_TABLE* SystemTable { get; set; }

    // A couple symbols the generated code will need we park them in this class
    // for no particular reason. These aid in transitioning to/from managed code.
    // Since we don't have a GC, the transition is a no-op.
    [RuntimeExport("RhpReversePInvoke")]
    static void RhpReversePInvoke(IntPtr frame) { }
    [RuntimeExport("RhpReversePInvokeReturn")]
    static void RhpReversePInvokeReturn(IntPtr frame) { }
    [RuntimeExport("RhpPInvoke")]
    static void RhpPInvoke(IntPtr frame) { }
    [RuntimeExport("RhpPInvokeReturn")]
    static void RhpPInvokeReturn(IntPtr frame) { }

    [RuntimeExport("RhpFallbackFailFast")]
    static void RhpFallbackFailFast() { while (true) ; }

    [RuntimeExport("__security_cookie")]
    static void __security_cookie() { }

    [RuntimeExport("RhpNewFast")]
    static unsafe void* RhpNewFast(MethodTable* pMT)
    {
      MethodTable** result = AllocObject(pMT->_uBaseSize);
      *result = pMT;
      return result;
    }

    [RuntimeExport("RhpNewArray")]
    static unsafe void* RhpNewArray(MethodTable* pMT, int numElements)
    {
      if (numElements < 0)
        RhpFallbackFailFast();

      MethodTable** result = AllocObject((uint)(pMT->_uBaseSize + numElements * pMT->_usComponentSize));
      *result = pMT;
      *(int*)(result + 1) = numElements;
      return result;
    }

    internal struct ArrayElement
    {
      public object Value;
    }

    [RuntimeExport("RhpStelemRef")]
    public static unsafe void StelemRef(Array array, nint index, object obj)
    {
      ref object element = ref Unsafe.As<ArrayElement[]>(array)[index].Value;

      MethodTable* elementType = array.m_pMethodTable->_relatedType;

      if (obj == null)
      {
        element = null;
        return;
      }

      if (elementType != obj.m_pMethodTable)
      {
        RhpFallbackFailFast();
      }

      element = obj;
    }

    [RuntimeExport("RhpCheckedAssignRef")]
    public static unsafe void RhpCheckedAssignRef(void** dst, void* r)
    {
      *dst = r;
    }

    [RuntimeExport("RhpAssignRef")]
    public static unsafe void RhpAssignRef(void** dst, void* r)
    {
      *dst = r;
    }

    static unsafe MethodTable** AllocObject(uint size)
    {
      MethodTable** result;
      SystemTable->BootServices->AllocatePool(EFI_MEMORY_TYPE.EfiLoaderData, size, (void**)&result);
      return result;
    }
  }
}
#endregion
