using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[DefaultMember("Name")]
[Guid("0002096B-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface _Document
{
	[DispId(0)]
	string Name
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	void _VtblGap1_289();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(376)]
	void SaveAs([Optional][In][MarshalAs(UnmanagedType.Struct)] ref object FileName, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object FileFormat, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object LockComments, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object Password, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object AddToRecentFiles, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object WritePassword, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object ReadOnlyRecommended, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object EmbedTrueTypeFonts, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object SaveNativePictureFormat, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object SaveFormsData, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object SaveAsAOCELetter, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object Encoding, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object InsertLineBreaks, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object AllowSubstitutions, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object LineEnding, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object AddBiDiMarks);
}
