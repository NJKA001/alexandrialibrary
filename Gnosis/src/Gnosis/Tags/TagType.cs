using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tags
{
    public class TagType
        : ITagType
    {
        private TagType(int id, string name, string code, TagDomain domain)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            
            this.id = id;
            this.name = name;
            this.code = code;
            this.domain = domain;
        }

        private readonly int id;
        private readonly string name;
        private readonly string code;
        private readonly TagDomain domain;

        public int Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Code
        {
            get { return code; }
        }

        public TagDomain Domain
        {
            get { return domain; }
        }

        public override string ToString()
        {
            return name;
        }

        static TagType()
        {
            all.Add(DefaultString);
            all.Add(DefaultPositiveInteger);
            all.Add(DefaultDateTime);
            all.Add(DefaultTimeSpan);
            all.Add(DefaultByteArray);

            all.Add(Year);
            all.Add(TrackCount);
            all.Add(DiscCount);
            all.Add(Genre);

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

        public static readonly ITagType DefaultString = new TagType(1, "Tag", string.Empty, TagDomain.String);
        public static readonly ITagType DefaultPositiveInteger = new TagType(2, "Tag", string.Empty, TagDomain.PositiveInteger);
        public static readonly ITagType DefaultDateTime = new TagType(3, "Tag", string.Empty, TagDomain.DateTime);
        public static readonly ITagType DefaultTimeSpan = new TagType(4, "Tag", string.Empty, TagDomain.TimeSpan);
        public static readonly ITagType DefaultByteArray = new TagType(5, "Tag", string.Empty, TagDomain.ByteArray);

        public static readonly ITagType Year = new TagType(101, "Year", string.Empty, TagDomain.PositiveInteger);
        public static readonly ITagType TrackCount = new TagType(102, "TrackCount", string.Empty, TagDomain.PositiveInteger);
        public static readonly ITagType DiscCount = new TagType(103, "DiscCount", string.Empty, TagDomain.PositiveInteger);
        public static readonly ITagType Genre = new TagType(104, "Genre", string.Empty, TagDomain.String);

        public static readonly ITagType AudioEncryption = new TagType(116, "Audio Encryption", "AENC", TagDomain.String);
        public static readonly ITagType AttachedPicture = new TagType(117, "Attached Picture", "APIC", TagDomain.ByteArray);
        public static readonly ITagType AudioSeekPointIndex = new TagType(118, "Audio Seek Point Index", "ASPI", TagDomain.String);
        public static readonly ITagType Comments = new TagType(119, "Comments", "COMM", TagDomain.String);
        public static readonly ITagType CommercialFrame = new TagType(120, "Commercial Frame", "COMR", TagDomain.String);
        public static readonly ITagType EncryptionMethod = new TagType(121, "Encryption Method Registration", "ENCR", TagDomain.String);
        public static readonly ITagType Equalization2 = new TagType(122, "Equalization", "EQU2", TagDomain.String);
        public static readonly ITagType EventTimingCodes = new TagType(123, "Event Timing Codes", "ETCO", TagDomain.String);
        public static readonly ITagType GeneralEncapsulatedObject = new TagType(124, "General Encapsulated Object", "GEOB", TagDomain.ByteArray);
        public static readonly ITagType GroupIdentificationRegistration = new TagType(125, "Group Identification Registration", "GRID", TagDomain.String);
        public static readonly ITagType LinkedInformation = new TagType(126, "Linked Information", "LINK", TagDomain.String);
        public static readonly ITagType MusicCdIdentifier = new TagType(127, "Music CD Identifier", "MCDI", TagDomain.String);
        public static readonly ITagType MpegLocationLookupTable = new TagType(128, "MPEG Location Lookup Table", "MLLT", TagDomain.String);
        public static readonly ITagType OwnershipFrame = new TagType(129, "Ownership Frame", "OWNE", TagDomain.String);
        public static readonly ITagType PrivateFrame = new TagType(130, "Private Frame", "PRIV", TagDomain.String);
        public static readonly ITagType PlayCounter = new TagType(131, "Play Counter", "PCNT", TagDomain.String);
        public static readonly ITagType Popularimeter = new TagType(132, "Popularimeter", "POPM", TagDomain.String);
        public static readonly ITagType PositionSynchronizationFrame = new TagType(133, "Position Synchronization Frame", "POSS", TagDomain.String);
        public static readonly ITagType RecommendedBufferSize = new TagType(134, "Recommended Buffer Size", "RBUF", TagDomain.String);
        public static readonly ITagType RelativeVolumeAdjustment = new TagType(135, "Relative Volume Adjustment", "RVA2", TagDomain.String);
        public static readonly ITagType Reverb = new TagType(136, "Reverb", "RVRB", TagDomain.String);
        public static readonly ITagType Seek = new TagType(137, "Seek Frame", "SEEK", TagDomain.String);
        public static readonly ITagType SignatureFrame = new TagType(138, "Signature Frame", "SIGN", TagDomain.String);
        public static readonly ITagType SynchronizedLyricsOrText = new TagType(139, "Synchronized Lyrics or Text", "SYLT", TagDomain.String);
        public static readonly ITagType SynchronizedTempoCodes = new TagType(140, "Synchronized Tempo Codes", "SYTC", TagDomain.String);
        public static readonly ITagType Album = new TagType(141, "Album", "TALB", TagDomain.String);

        public static readonly ITagType BeatsPerMinute = new TagType(142, "BPM (1beats per minute)", "TBPM", TagDomain.String);
        public static readonly ITagType Composer = new TagType(143, "Composer", "TCOM", TagDomain.String);
        public static readonly ITagType ContentType = new TagType(144, "Content Type", "TCON", TagDomain.String);
        public static readonly ITagType CopyrightMessage = new TagType(145, "Copyright Message", "TCOP", TagDomain.String);
        public static readonly ITagType EncodingTime = new TagType(146, "Encoding Time", "TDEN", TagDomain.String);
        public static readonly ITagType PlaylistDelay = new TagType(147, "Playlist Delay", "TDLY", TagDomain.TimeSpan);
        public static readonly ITagType OriginalReleaseTime = new TagType(148, "Original Release time", "TDOR", TagDomain.DateTime);
        public static readonly ITagType RecordingTime = new TagType(149, "Recording Time", "TDRC", TagDomain.DateTime);
        public static readonly ITagType ReleaseTime = new TagType(150, "Release Time", "TDRL", TagDomain.DateTime);
        public static readonly ITagType TaggingTime = new TagType(151, "Tagging Time", "TDTG", TagDomain.DateTime);
        public static readonly ITagType EncodedBy = new TagType(152, "Encoded By", "TENC", TagDomain.String);
        public static readonly ITagType LyricistOrWriter = new TagType(153, "Lyricist or Writer", "TEXT", TagDomain.String);
        public static readonly ITagType FileType = new TagType(154, "File Type", "TFLT", TagDomain.String);
        public static readonly ITagType InvolvedPeopleList = new TagType(155, "Involved People List", "TIPL", TagDomain.String);
        public static readonly ITagType ContentGroupDescription = new TagType(156, "Content Group Description", "TIT1", TagDomain.String);
        public static readonly ITagType Title = new TagType(157, "Title", "TIT2", TagDomain.String);
        public static readonly ITagType Subtitle = new TagType(158, "Subtitle", "TIT3", TagDomain.String);
        public static readonly ITagType InitialKey = new TagType(159, "Initial Key", "TKEY", TagDomain.String);
        public static readonly ITagType Languages = new TagType(160, "Language(1s)", "TLAN", TagDomain.String);
        public static readonly ITagType Length = new TagType(161, "Length", "TLEN", TagDomain.String);
        public static readonly ITagType MusicianCreditsList = new TagType(162, "Musician Credits List", "TMCL", TagDomain.String);
        public static readonly ITagType MediaType = new TagType(163, "Media Type", "TMED", TagDomain.String);
        public static readonly ITagType Mood = new TagType(164, "Mood", "TMOO", TagDomain.String);
        public static readonly ITagType OriginalAlbumTitle = new TagType(165, "Original Album Title", "TOAL", TagDomain.String);
        public static readonly ITagType OriginalFileName = new TagType(166, "Original Filename", "TOFN", TagDomain.String);
        public static readonly ITagType OriginalLyricstsOrWriters = new TagType(167, "Original Lyricists or Writers", "TOLY", TagDomain.String);
        public static readonly ITagType OriginalArtists = new TagType(168, "Original Artists", "TOPE", TagDomain.String);
        public static readonly ITagType FileOwner = new TagType(169, "File Owner", "TOWN", TagDomain.String);
        public static readonly ITagType Artist = new TagType(170, "Artist", "TPE1", TagDomain.String);
        public static readonly ITagType BandAccompaniment = new TagType(171, "Band Accompaniment", "TPE2", TagDomain.String);
        public static readonly ITagType Conductor = new TagType(172, "Conductor", "TPE3", TagDomain.String);
        public static readonly ITagType InterpretedOrRemixedBy = new TagType(173, "Interpreted or Remixed By", "TPE4", TagDomain.String);
        public static readonly ITagType PartOfASet = new TagType(174, "Part of a Set", "TPOS", TagDomain.String);
        public static readonly ITagType ProducedNotice = new TagType(175, "Produced Notice", "TPRO", TagDomain.String);
        public static readonly ITagType Publisher = new TagType(176, "Publisher", "TPUB", TagDomain.String);
        public static readonly ITagType TrackNumber = new TagType(177, "Track Number", "TRCK", TagDomain.String);
        public static readonly ITagType InternetRadioStationName = new TagType(178, "Internet Radio Station Name", "TRSN", TagDomain.String);
        public static readonly ITagType InternetRadioStationOwner = new TagType(179, "Internet Radio Station Owner", "TRSO", TagDomain.String);
        public static readonly ITagType AlbumSortOrder = new TagType(180, "Album Sort Order", "TSOA", TagDomain.String);
        public static readonly ITagType PerformerSortOrder = new TagType(181, "Performer Sort Order", "TSOP", TagDomain.String);
        public static readonly ITagType TitleSortOrder = new TagType(182, "Title Sort Order", "TSOT", TagDomain.String);
        public static readonly ITagType InternationalStandardRecordingCode = new TagType(183, "ISRC (1International Standard Recording Code)", "TSRC", TagDomain.String);
        public static readonly ITagType EncodingSettings = new TagType(184, "Settings Used for Encoding", "TSSE", TagDomain.String);
        public static readonly ITagType SetSubtitle = new TagType(185, "Set Subtitle", "TSST", TagDomain.String);
        public static readonly ITagType UserDefinedText = new TagType(186, "User Defined Text", "TXXX", TagDomain.String);

        public static readonly ITagType UniqueFileIdentifier = new TagType(187, "Unique File Identifier", "UFID", TagDomain.String);
        public static readonly ITagType TermsOfUse = new TagType(188, "Terms of Use", "USER", TagDomain.String);
        public static readonly ITagType LyricsOrText = new TagType(189, "Lyrics or Text", "USLT", TagDomain.String);

        public static readonly ITagType CommercialInformationLink = new TagType(190, "Commercial Information", "WCOM", TagDomain.String);
        public static readonly ITagType CopyrightLink = new TagType(191, "Copyright Information", "WCOP", TagDomain.String);
        public static readonly ITagType OfficialAudioFileLink = new TagType(192, "Official Audio File Webpage", "WOAF", TagDomain.String);
        public static readonly ITagType OfficialArtistLink = new TagType(193, "Official Artist Webpage", "WOAR", TagDomain.String);
        public static readonly ITagType OfficialAudioSourceLink = new TagType(194, "Official Audio Source Webpage", "WOAS", TagDomain.String);
        public static readonly ITagType OfficialInternetRadioStationLink = new TagType(195, "Official Internet Radio Station Homepage", "WORS", TagDomain.String);
        public static readonly ITagType PaymentLink = new TagType(196, "Payment Webpage", "WPAY", TagDomain.String);
        public static readonly ITagType PublisherLink = new TagType(197, "Publishers Official Webpage", "WPUB", TagDomain.String);
        public static readonly ITagType UserDefinedLink = new TagType(198, "User Defined Link", "WXXX", TagDomain.String);

        public static IEnumerable<ITagType> GetAll()
        {
            return all;
        }
    }
}
