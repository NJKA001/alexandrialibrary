using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Alexandria.MusicBrainz
{
	internal static class NativeMethods
	{
		#region Trm Methods
		[DllImport("musicbrainz")]
		internal static extern IntPtr trm_New();
		
		[DllImport("musicbrainz")]
		internal static extern void trm_Delete(IntPtr handle);

		[DllImport("musicbrainz")]
		internal static extern void trm_SetProxy(IntPtr handle, byte[] proxyAddress, short proxyPort);

		[DllImport("musicbrainz")]
		internal static extern int trm_SetPCMDataInfo(IntPtr handle, int samplesPerSecond, int numberOfChannels, int bitsPerSample);

		[DllImport("musicbrainz")]
		internal static extern void trm_SetSongLength(IntPtr handle, long seconds);

		[DllImport("musicbrainz")]
		internal static extern int trm_GenerateSignature(IntPtr handle, byte[] data, int size);

		[DllImport("musicbrainz")]
		internal static extern int trm_FinalizeSignature(IntPtr handle, byte[] signature, byte[] collectionId);

		[DllImport("musicbrainz")]
		internal static extern int trm_ConvertSigToASCII(IntPtr handle, byte[] signature, byte[] asciiSignature);
		#endregion

		#region WindowsNetworkControl Methods
		[DllImport("musicbrainz")]
		internal static extern int mb_WSAInit(IntPtr handle);

		[DllImport("musicbrainz")]
		internal static extern int mb_WSAStop(IntPtr handle);
		#endregion

		#region MusicBrainzClient Methods
		[DllImport("musicbrainz")]
		internal static extern IntPtr mb_New();

		[DllImport("musicbrainz")]
		internal static extern void mb_Delete(IntPtr handle);

		[DllImport("musicbrainz")]
		internal static extern void mb_UseUTF8(IntPtr handle, int value);

		[DllImport("musicbrainz")]
		internal static extern void mb_GetVersion(IntPtr handle, out int major, out int minor, out int revision);

		[DllImport("musicbrainz")]
		internal static extern int mb_SetServer(IntPtr handle, byte[] serverAddress, short serverPort);

		[DllImport("musicbrainz")]
		internal static extern int mb_SetProxy(IntPtr handle, byte[] serverAddress, short serverPort);

		[DllImport("musicbrainz")]
		internal static extern int mb_Authenticate(IntPtr handle, byte[] userName, byte[] password);

		[DllImport("musicbrainz")]
		internal static extern int mb_SetDevice(IntPtr handle, byte[] device);

		[DllImport("musicbrainz")]
		internal static extern void mb_SetDepth(IntPtr handle, int depth);

		[DllImport("musicbrainz")]
		internal static extern void mb_SetMaxItems(IntPtr handle, int maximumItems);

		[DllImport("musicbrainz")]
		internal static extern int mb_Query(IntPtr handle, byte[] rdfObject);

		[DllImport("musicbrainz")]
		internal static extern int mb_QueryWithArgs(IntPtr handle, byte[] rdfObject, IntPtr[] args);

		[DllImport("musicbrainz")]
		internal static extern int mb_GetQueryError(IntPtr handle, byte[] error, int errorLength);

		[DllImport("musicbrainz")]
		internal static extern int mb_GetWebSubmitURL(IntPtr handle, byte[] url, int urlLength);

		[DllImport("musicbrainz")]
		internal static extern int mb_Select1(IntPtr handle, byte[] selectQuery, int ordinal);

		[DllImport("musicbrainz")]
		internal static extern int mb_SelectWithArgs(IntPtr handle, byte[] selectQuery, int[] ordinals);

		[DllImport("musicbrainz")]
		internal static extern int mb_DoesResultExist1(IntPtr handle, byte[] resultName, int ordinal);

		[DllImport("musicbrainz")]
		internal static extern int mb_GetResultData1(IntPtr handle, byte[] resultName, byte[] data, int dataLength, int ordinal);

		[DllImport("musicbrainz")]
		internal static extern int mb_GetResultInt1(IntPtr handle, byte[] resultName, int ordinal);

		[DllImport("musicbrainz")]
		internal static extern int mb_GetResultRDF(IntPtr handle, byte[] rdf, int rdfLength);

		[DllImport("musicbrainz")]
		internal static extern int mb_GetResultRDFLen(IntPtr handle);

		[DllImport("musicbrainz")]
		internal static extern int mb_SetResultRDF(IntPtr handle, byte[] rdf);

		[DllImport("musicbrainz")]
		internal static extern void mb_GetIDFromURL(IntPtr handle, byte[] url, byte[] id, int idLength);

		[DllImport("musicbrainz")]
		internal static extern void mb_GetFragmentFromURL(IntPtr handle, byte[] url, byte[] fragment, int fragmentLength);

		[DllImport("musicbrainz")]
		internal static extern int mb_GetOrdinalFromList(IntPtr handle, byte[] resultList, byte[] uri);

		[DllImport("musicbrainz")]
		internal static extern int mb_GetMP3Info(IntPtr handle, byte[] fileName, out int duration, out int bitRate, out int stereo, out int sampleRate);

		[DllImport("musicbrainz")]
		internal static extern void mb_SetDebug(IntPtr handle, int debug);
		#endregion
	}
}
