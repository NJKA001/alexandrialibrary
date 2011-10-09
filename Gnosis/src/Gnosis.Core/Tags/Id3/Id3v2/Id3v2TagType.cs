using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Tags.Id3.Id3v2
{
    public class Id3v2TagType
        : TagType
    {
        private Id3v2TagType(int id, string name, string code, ITagDomain domain)
            : base(id, name, TagSchema.Id3v2, domain)
        {
            if (code == null)
                throw new ArgumentNullException("code");

            this.code = code;
        }

        private readonly string code;

        public string Code
        {
            get { return code; }
        }

        static Id3v2TagType()
        {
            all.Add(AudioEncryption);
            all.Add(AttachedPicture);
            all.Add(AudioSeekPointIndex);
            all.Add(Comments);
            all.Add(CommercialFrame);
            all.Add(EncryptionMethod);
            all.Add(Equalization2);
            all.Add(EventTimingCodes);
            all.Add(GeneralEncapsulatedObject);
            all.Add(GroupIdentificationRegistration);
            all.Add(LinkedInformation);
            all.Add(MusicCdIdentifier);
            all.Add(MpegLocationLookupTable);
            all.Add(OwnershipFrame);
            all.Add(PrivateFrame);
            all.Add(PlayCounter);
            all.Add(Popularimeter);
            all.Add(PositionSynchronizationFrame);
            all.Add(RecommendedBufferSize);
            all.Add(RelativeVolumeAdjustment);
            all.Add(Reverb);
            all.Add(Seek);
            all.Add(SignatureFrame);
            all.Add(SynchronizedLyricsOrText);
            all.Add(SynchronizedTempoCodes);
            all.Add(Album);
            all.Add(BeatsPerMinute);
            all.Add(Composer);
            all.Add(ContentType);
            all.Add(CopyrightMessage);
            all.Add(EncodingTime);
            all.Add(PlaylistDelay);
            all.Add(OriginalReleaseTime);
            all.Add(RecordingTime);
            all.Add(ReleaseTime);
            all.Add(TaggingTime);
            all.Add(EncodedBy);
            all.Add(LyricistOrWriter);
            all.Add(FileType);
            all.Add(InvolvedPeopleList);
            all.Add(ContentGroupDescription);
            all.Add(Title);
            all.Add(Subtitle);
            all.Add(InitialKey);
            all.Add(Languages);
            all.Add(Length);
            all.Add(MusicianCreditsList);
            all.Add(MediaType);
            all.Add(Mood);
            all.Add(OriginalAlbumTitle);
            all.Add(OriginalFileName);
            all.Add(OriginalLyricstsOrWriters);
            all.Add(OriginalArtists);
            all.Add(FileOwner);
            all.Add(Artist);
            all.Add(BandAccompaniment);
            all.Add(Conductor);
            all.Add(InterpretedOrRemixedBy);
            all.Add(PartOfASet);
            all.Add(ProducedNotice);
            all.Add(Publisher);
            all.Add(TrackNumber);
            all.Add(InternetRadioStationName);
            all.Add(InternetRadioStationOwner);
            all.Add(AlbumSortOrder);
            all.Add(PerformerSortOrder);
            all.Add(TitleSortOrder);
            all.Add(InternationalStandardRecordingCode);
            all.Add(EncodingSettings);
            all.Add(SetSubtitle);
            all.Add(UserDefinedText);
            all.Add(UniqueFileIdentifier);
            all.Add(TermsOfUse);
            all.Add(LyricsOrText);
            all.Add(CommercialInformationLink);
            all.Add(CopyrightLink);
            all.Add(OfficialAudioFileLink);
            all.Add(OfficialArtistLink);
            all.Add(OfficialAudioSourceLink);
            all.Add(OfficialInternetRadioStationLink);
            all.Add(PaymentLink);
            all.Add(PublisherLink);
            all.Add(UserDefinedLink);
        }

        private static readonly IList<ITagType> all = new List<ITagType>();

        public static ITagType AudioEncryption = new Id3v2TagType(16, "Audio Encryption", "AENC", TagDomain.String);
        public static ITagType AttachedPicture = new Id3v2TagType(17, "Attached Picture", "APIC", TagDomain.String);
        public static ITagType AudioSeekPointIndex = new Id3v2TagType(18, "Audio Seek Point Index", "ASPI", TagDomain.String);
        public static ITagType Comments = new Id3v2TagType(19, "Comments", "COMM", TagDomain.String);
        public static ITagType CommercialFrame = new Id3v2TagType(20, "Commercial Frame", "COMR", TagDomain.String);
        public static ITagType EncryptionMethod = new Id3v2TagType(21, "Encryption Method Registration", "ENCR", TagDomain.String);
        public static ITagType Equalization2 = new Id3v2TagType(22, "Equalization", "EQU2", TagDomain.String);
        public static ITagType EventTimingCodes = new Id3v2TagType(23, "Event Timing Codes", "ETCO", TagDomain.String);
        public static ITagType GeneralEncapsulatedObject = new Id3v2TagType(24, "General Encapsulated Object", "GEOB", TagDomain.String);
        public static ITagType GroupIdentificationRegistration = new Id3v2TagType(25, "Group Identification Registration", "GRID", TagDomain.String);
        public static ITagType LinkedInformation = new Id3v2TagType(26, "Linked Information", "LINK", TagDomain.String);
        public static ITagType MusicCdIdentifier = new Id3v2TagType(27, "Music CD Identifier", "MCDI", TagDomain.String);
        public static ITagType MpegLocationLookupTable = new Id3v2TagType(28, "MPEG Location Lookup Table", "MLLT", TagDomain.String);
        public static ITagType OwnershipFrame = new Id3v2TagType(29, "Ownership Frame", "OWNE", TagDomain.String);
        public static ITagType PrivateFrame = new Id3v2TagType(30, "Private Frame", "PRIV", TagDomain.String);
        public static ITagType PlayCounter = new Id3v2TagType(31, "Play Counter", "PCNT", TagDomain.String);
        public static ITagType Popularimeter = new Id3v2TagType(32, "Popularimeter", "POPM", TagDomain.String);
        public static ITagType PositionSynchronizationFrame = new Id3v2TagType(33, "Position Synchronization Frame", "POSS", TagDomain.String);
        public static ITagType RecommendedBufferSize = new Id3v2TagType(34, "Recommended Buffer Size", "RBUF", TagDomain.String);
        public static ITagType RelativeVolumeAdjustment = new Id3v2TagType(35, "Relative Volume Adjustment", "RVA2", TagDomain.String);
        public static ITagType Reverb = new Id3v2TagType(36, "Reverb", "RVRB", TagDomain.String);
        public static ITagType Seek = new Id3v2TagType(37, "Seek Frame", "SEEK", TagDomain.String);
        public static ITagType SignatureFrame = new Id3v2TagType(38, "Signature Frame", "SIGN", TagDomain.String);
        public static ITagType SynchronizedLyricsOrText = new Id3v2TagType(39, "Synchronized Lyrics or Text", "SYLT", TagDomain.String);
        public static ITagType SynchronizedTempoCodes = new Id3v2TagType(40, "Synchronized Tempo Codes", "SYTC", TagDomain.String);
        public static ITagType Album = new Id3v2TagType(41, "Album", "TALB", TagDomain.String);
        
        public static ITagType BeatsPerMinute = new Id3v2TagType(42 , "BPM (beats per minute)", "TBPM", TagDomain.String);
        public static ITagType Composer = new Id3v2TagType(43 , "Composer", "TCOM", TagDomain.String);
        public static ITagType ContentType = new Id3v2TagType(44 , "Content Type", "TCON", TagDomain.String);
        public static ITagType CopyrightMessage = new Id3v2TagType(45 , "Copyright Message", "TCOP", TagDomain.String);
        public static ITagType EncodingTime = new Id3v2TagType(46 , "Encoding Time", "TDEN", TagDomain.String);
        public static ITagType PlaylistDelay = new Id3v2TagType(47 , "Playlist Delay", "TDLY", TagDomain.String);
        public static ITagType OriginalReleaseTime = new Id3v2TagType(48 , "Original Release time", "TDOR", TagDomain.String);
        public static ITagType RecordingTime = new Id3v2TagType(49 , "Recording Time", "TDRC", TagDomain.String);
        public static ITagType ReleaseTime = new Id3v2TagType(50 , "Release Time", "TDRL", TagDomain.String);
        public static ITagType TaggingTime = new Id3v2TagType(51 , "Tagging Time", "TDTG", TagDomain.String);
        public static ITagType EncodedBy = new Id3v2TagType(52 , "Encoded By", "TENC", TagDomain.String);
        public static ITagType LyricistOrWriter = new Id3v2TagType(53 , "Lyricist or Writer", "TEXT", TagDomain.String);
        public static ITagType FileType = new Id3v2TagType(54 , "File Type", "TFLT", TagDomain.String);
        public static ITagType InvolvedPeopleList = new Id3v2TagType(55 , "Involved People List", "TIPL", TagDomain.String);
        public static ITagType ContentGroupDescription = new Id3v2TagType(56 , "Content Group Description", "TIT1", TagDomain.String);
        public static ITagType Title = new Id3v2TagType(57 , "Title", "TIT2", TagDomain.String);
        public static ITagType Subtitle = new Id3v2TagType(58 , "Subtitle", "TIT3", TagDomain.String);
        public static ITagType InitialKey = new Id3v2TagType(59 , "Initial Key", "TKEY", TagDomain.String);
        public static ITagType Languages = new Id3v2TagType(60 , "Language(s)", "TLAN", TagDomain.String);
        public static ITagType Length = new Id3v2TagType(61 , "Length", "TLEN", TagDomain.String);
        public static ITagType MusicianCreditsList = new Id3v2TagType(62 , "Musician Credits List", "TMCL", TagDomain.String);
        public static ITagType MediaType = new Id3v2TagType(63 , "Media Type", "TMED", TagDomain.String);
        public static ITagType Mood = new Id3v2TagType(64 , "Mood", "TMOO", TagDomain.String);
        public static ITagType OriginalAlbumTitle = new Id3v2TagType(65 , "Original Album Title", "TOAL", TagDomain.String);
        public static ITagType OriginalFileName = new Id3v2TagType(66 , "Original Filename", "TOFN", TagDomain.String);
        public static ITagType OriginalLyricstsOrWriters = new Id3v2TagType(67 , "Original Lyricists or Writers", "TOLY", TagDomain.String);
        public static ITagType OriginalArtists = new Id3v2TagType(68 , "Original Artists", "TOPE", TagDomain.String);
        public static ITagType FileOwner = new Id3v2TagType(69 , "File Owner", "TOWN", TagDomain.String);
        public static ITagType Artist = new Id3v2TagType(70 , "Artist", "TPE1", TagDomain.String);
        public static ITagType BandAccompaniment = new Id3v2TagType(71 , "Band Accompaniment", "TPE2", TagDomain.String);
        public static ITagType Conductor = new Id3v2TagType(72 , "Conductor", "TPE3", TagDomain.String);
        public static ITagType InterpretedOrRemixedBy = new Id3v2TagType(73 , "Interpreted or Remixed By", "TPE4", TagDomain.String);
        public static ITagType PartOfASet = new Id3v2TagType(74 , "Part of a Set", "TPOS", TagDomain.String);
        public static ITagType ProducedNotice = new Id3v2TagType(75 , "Produced Notice", "TPRO", TagDomain.String);
        public static ITagType Publisher = new Id3v2TagType(76 , "Publisher", "TPUB", TagDomain.String);
        public static ITagType TrackNumber = new Id3v2TagType(77 , "Track Number", "TRCK", TagDomain.String);
        public static ITagType InternetRadioStationName = new Id3v2TagType(78 , "Internet Radio Station Name", "TRSN", TagDomain.String);
        public static ITagType InternetRadioStationOwner = new Id3v2TagType(79 , "Internet Radio Station Owner", "TRSO", TagDomain.String);
        public static ITagType AlbumSortOrder = new Id3v2TagType(80 , "Album Sort Order", "TSOA", TagDomain.String);
        public static ITagType PerformerSortOrder = new Id3v2TagType(81 , "Performer Sort Order", "TSOP", TagDomain.String);
        public static ITagType TitleSortOrder = new Id3v2TagType(82 , "Title Sort Order", "TSOT", TagDomain.String);
        public static ITagType InternationalStandardRecordingCode = new Id3v2TagType(83 , "ISRC (International Standard Recording Code)", "TSRC", TagDomain.String);
        public static ITagType EncodingSettings = new Id3v2TagType(84 , "Settings Used for Encoding", "TSSE", TagDomain.String);
        public static ITagType SetSubtitle = new Id3v2TagType(85 , "Set Subtitle", "TSST", TagDomain.String);
        public static ITagType UserDefinedText = new Id3v2TagType(86 , "User Defined Text", "TXXX", TagDomain.String);

        public static ITagType UniqueFileIdentifier = new Id3v2TagType(87 , "Unique File Identifier", "UFID", TagDomain.String);
        public static ITagType TermsOfUse = new Id3v2TagType(88 , "Terms of Use", "USER", TagDomain.String);
        public static ITagType LyricsOrText = new Id3v2TagType(89 , "Lyrics or Text", "USLT", TagDomain.String);

        public static ITagType CommercialInformationLink = new Id3v2TagType(90 , "Commercial Information", "WCOM", TagDomain.String);
        public static ITagType CopyrightLink = new Id3v2TagType(91 , "Copyright Information", "WCOP", TagDomain.String);
        public static ITagType OfficialAudioFileLink = new Id3v2TagType(92 , "Official Audio File Webpage", "WOAF", TagDomain.String);
        public static ITagType OfficialArtistLink = new Id3v2TagType(93 , "Official Artist Webpage", "WOAR", TagDomain.String);
        public static ITagType OfficialAudioSourceLink = new Id3v2TagType(94 , "Official Audio Source Webpage", "WOAS", TagDomain.String);
        public static ITagType OfficialInternetRadioStationLink = new Id3v2TagType(95 , "Official Internet Radio Station Homepage", "WORS", TagDomain.String);
        public static ITagType PaymentLink = new Id3v2TagType(96 , "Payment Webpage", "WPAY", TagDomain.String);
        public static ITagType PublisherLink = new Id3v2TagType(97 , "Publishers Official Webpage", "WPUB", TagDomain.String); 
        public static ITagType UserDefinedLink = new Id3v2TagType(98 , "User Defined Link", "WXXX", TagDomain.String);

        public static IEnumerable<ITagType> GetAll()
        {
            return all;
        }
    }
}
