using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Tags.Id3.Id3v2
{
    public class Id3v2TagType
    {
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

        public static ITagType AudioEncryption = new Id3v2TagType<string>(116, "Audio Encryption", "AENC", TagDomain.String);
        public static ITagType AttachedPicture = new Id3v2TagType<byte[]>(117, "Attached Picture", "APIC", TagDomain.ByteArray);
        public static ITagType AudioSeekPointIndex = new Id3v2TagType<string>(118, "Audio Seek Point Index", "ASPI", TagDomain.String);
        public static ITagType Comments = new Id3v2TagType<string>(119, "Comments", "COMM", TagDomain.String);
        public static ITagType CommercialFrame = new Id3v2TagType<string>(120, "Commercial Frame", "COMR", TagDomain.String);
        public static ITagType EncryptionMethod = new Id3v2TagType<string>(121, "Encryption Method Registration", "ENCR", TagDomain.String);
        public static ITagType Equalization2 = new Id3v2TagType<string>(122, "Equalization", "EQU2", TagDomain.String);
        public static ITagType EventTimingCodes = new Id3v2TagType<string>(123, "Event Timing Codes", "ETCO", TagDomain.String);
        public static ITagType GeneralEncapsulatedObject = new Id3v2TagType<byte[]>(124, "General Encapsulated Object", "GEOB", TagDomain.String);
        public static ITagType GroupIdentificationRegistration = new Id3v2TagType<string>(125, "Group Identification Registration", "GRID", TagDomain.String);
        public static ITagType LinkedInformation = new Id3v2TagType<string>(126, "Linked Information", "LINK", TagDomain.String);
        public static ITagType MusicCdIdentifier = new Id3v2TagType<string>(127, "Music CD Identifier", "MCDI", TagDomain.String);
        public static ITagType MpegLocationLookupTable = new Id3v2TagType<string>(128, "MPEG Location Lookup Table", "MLLT", TagDomain.String);
        public static ITagType OwnershipFrame = new Id3v2TagType<string>(129, "Ownership Frame", "OWNE", TagDomain.String);
        public static ITagType PrivateFrame = new Id3v2TagType<string>(130, "Private Frame", "PRIV", TagDomain.String);
        public static ITagType PlayCounter = new Id3v2TagType<string>(131, "Play Counter", "PCNT", TagDomain.String);
        public static ITagType Popularimeter = new Id3v2TagType<string>(132, "Popularimeter", "POPM", TagDomain.String);
        public static ITagType PositionSynchronizationFrame = new Id3v2TagType<string>(133, "Position Synchronization Frame", "POSS", TagDomain.String);
        public static ITagType RecommendedBufferSize = new Id3v2TagType<string>(134, "Recommended Buffer Size", "RBUF", TagDomain.String);
        public static ITagType RelativeVolumeAdjustment = new Id3v2TagType<string>(135, "Relative Volume Adjustment", "RVA2", TagDomain.String);
        public static ITagType Reverb = new Id3v2TagType<string>(136, "Reverb", "RVRB", TagDomain.String);
        public static ITagType Seek = new Id3v2TagType<string>(137, "Seek Frame", "SEEK", TagDomain.String);
        public static ITagType SignatureFrame = new Id3v2TagType<string>(138, "Signature Frame", "SIGN", TagDomain.String);
        public static ITagType SynchronizedLyricsOrText = new Id3v2TagType<string>(139, "Synchronized Lyrics or Text", "SYLT", TagDomain.String);
        public static ITagType SynchronizedTempoCodes = new Id3v2TagType<string>(140, "Synchronized Tempo Codes", "SYTC", TagDomain.String);
        public static ITagType Album = new Id3v2TagType<string>(141, "Album", "TALB", TagDomain.String);

        public static ITagType BeatsPerMinute = new Id3v2TagType<string>(142, "BPM (1beats per minute)", "TBPM", TagDomain.String);
        public static ITagType Composer = new Id3v2TagType<string>(143, "Composer", "TCOM", TagDomain.String);
        public static ITagType ContentType = new Id3v2TagType<string>(144, "Content Type", "TCON", TagDomain.String);
        public static ITagType CopyrightMessage = new Id3v2TagType<string>(145, "Copyright Message", "TCOP", TagDomain.String);
        public static ITagType EncodingTime = new Id3v2TagType<string>(146, "Encoding Time", "TDEN", TagDomain.String);
        public static ITagType PlaylistDelay = new Id3v2TagType<string>(147, "Playlist Delay", "TDLY", TagDomain.String);
        public static ITagType OriginalReleaseTime = new Id3v2TagType<string>(148, "Original Release time", "TDOR", TagDomain.String);
        public static ITagType RecordingTime = new Id3v2TagType<string>(149, "Recording Time", "TDRC", TagDomain.String);
        public static ITagType ReleaseTime = new Id3v2TagType<string>(150, "Release Time", "TDRL", TagDomain.Date);
        public static ITagType TaggingTime = new Id3v2TagType<string>(151, "Tagging Time", "TDTG", TagDomain.String);
        public static ITagType EncodedBy = new Id3v2TagType<string>(152, "Encoded By", "TENC", TagDomain.String);
        public static ITagType LyricistOrWriter = new Id3v2TagType<string>(153, "Lyricist or Writer", "TEXT", TagDomain.String);
        public static ITagType FileType = new Id3v2TagType<string>(154, "File Type", "TFLT", TagDomain.String);
        public static ITagType InvolvedPeopleList = new Id3v2TagType<string>(155, "Involved People List", "TIPL", TagDomain.String);
        public static ITagType ContentGroupDescription = new Id3v2TagType<string>(156, "Content Group Description", "TIT1", TagDomain.String);
        public static ITagType Title = new Id3v2TagType<string>(157, "Title", "TIT2", TagDomain.String);
        public static ITagType Subtitle = new Id3v2TagType<string>(158, "Subtitle", "TIT3", TagDomain.String);
        public static ITagType InitialKey = new Id3v2TagType<string>(159, "Initial Key", "TKEY", TagDomain.String);
        public static ITagType Languages = new Id3v2TagType<string>(160, "Language(1s)", "TLAN", TagDomain.String);
        public static ITagType Length = new Id3v2TagType<string>(161, "Length", "TLEN", TagDomain.String);
        public static ITagType MusicianCreditsList = new Id3v2TagType<string>(162, "Musician Credits List", "TMCL", TagDomain.String);
        public static ITagType MediaType = new Id3v2TagType<string>(163, "Media Type", "TMED", TagDomain.String);
        public static ITagType Mood = new Id3v2TagType<string>(164, "Mood", "TMOO", TagDomain.String);
        public static ITagType OriginalAlbumTitle = new Id3v2TagType<string>(165, "Original Album Title", "TOAL", TagDomain.String);
        public static ITagType OriginalFileName = new Id3v2TagType<string>(166, "Original Filename", "TOFN", TagDomain.String);
        public static ITagType OriginalLyricstsOrWriters = new Id3v2TagType<string>(167, "Original Lyricists or Writers", "TOLY", TagDomain.String);
        public static ITagType OriginalArtists = new Id3v2TagType<string>(168, "Original Artists", "TOPE", TagDomain.String);
        public static ITagType FileOwner = new Id3v2TagType<string>(169, "File Owner", "TOWN", TagDomain.String);
        public static ITagType Artist = new Id3v2TagType<string>(170, "Artist", "TPE1", TagDomain.StringArray);
        public static ITagType BandAccompaniment = new Id3v2TagType<string>(171, "Band Accompaniment", "TPE2", TagDomain.String);
        public static ITagType Conductor = new Id3v2TagType<string>(172, "Conductor", "TPE3", TagDomain.String);
        public static ITagType InterpretedOrRemixedBy = new Id3v2TagType<string>(173, "Interpreted or Remixed By", "TPE4", TagDomain.String);
        public static ITagType PartOfASet = new Id3v2TagType<string>(174, "Part of a Set", "TPOS", TagDomain.String);
        public static ITagType ProducedNotice = new Id3v2TagType<string>(175, "Produced Notice", "TPRO", TagDomain.String);
        public static ITagType Publisher = new Id3v2TagType<string>(176, "Publisher", "TPUB", TagDomain.String);
        public static ITagType TrackNumber = new Id3v2TagType<string>(177, "Track Number", "TRCK", TagDomain.String);
        public static ITagType InternetRadioStationName = new Id3v2TagType<string>(178, "Internet Radio Station Name", "TRSN", TagDomain.String);
        public static ITagType InternetRadioStationOwner = new Id3v2TagType<string>(179, "Internet Radio Station Owner", "TRSO", TagDomain.String);
        public static ITagType AlbumSortOrder = new Id3v2TagType<string>(180, "Album Sort Order", "TSOA", TagDomain.String);
        public static ITagType PerformerSortOrder = new Id3v2TagType<string>(181, "Performer Sort Order", "TSOP", TagDomain.String);
        public static ITagType TitleSortOrder = new Id3v2TagType<string>(182, "Title Sort Order", "TSOT", TagDomain.String);
        public static ITagType InternationalStandardRecordingCode = new Id3v2TagType<string>(183, "ISRC (1International Standard Recording Code)", "TSRC", TagDomain.String);
        public static ITagType EncodingSettings = new Id3v2TagType<string>(184, "Settings Used for Encoding", "TSSE", TagDomain.String);
        public static ITagType SetSubtitle = new Id3v2TagType<string>(185, "Set Subtitle", "TSST", TagDomain.String);
        public static ITagType UserDefinedText = new Id3v2TagType<string>(186, "User Defined Text", "TXXX", TagDomain.String);

        public static ITagType UniqueFileIdentifier = new Id3v2TagType<string>(187, "Unique File Identifier", "UFID", TagDomain.String);
        public static ITagType TermsOfUse = new Id3v2TagType<string>(188, "Terms of Use", "USER", TagDomain.String);
        public static ITagType LyricsOrText = new Id3v2TagType<string>(189, "Lyrics or Text", "USLT", TagDomain.String);

        public static ITagType CommercialInformationLink = new Id3v2TagType<string>(190, "Commercial Information", "WCOM", TagDomain.String);
        public static ITagType CopyrightLink = new Id3v2TagType<string>(191, "Copyright Information", "WCOP", TagDomain.String);
        public static ITagType OfficialAudioFileLink = new Id3v2TagType<string>(192, "Official Audio File Webpage", "WOAF", TagDomain.String);
        public static ITagType OfficialArtistLink = new Id3v2TagType<string>(193, "Official Artist Webpage", "WOAR", TagDomain.String);
        public static ITagType OfficialAudioSourceLink = new Id3v2TagType<string>(194, "Official Audio Source Webpage", "WOAS", TagDomain.String);
        public static ITagType OfficialInternetRadioStationLink = new Id3v2TagType<string>(195, "Official Internet Radio Station Homepage", "WORS", TagDomain.String);
        public static ITagType PaymentLink = new Id3v2TagType<string>(196, "Payment Webpage", "WPAY", TagDomain.String);
        public static ITagType PublisherLink = new Id3v2TagType<string>(197, "Publishers Official Webpage", "WPUB", TagDomain.String);
        public static ITagType UserDefinedLink = new Id3v2TagType<string>(198, "User Defined Link", "WXXX", TagDomain.String);

        public static IEnumerable<ITagType> GetAll()
        {
            return all;
        }
    }

    public class Id3v2TagType<T>
        : TagType<T>
    {
        public Id3v2TagType(int id, string name, string code, ITagDomain domain)
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
    }
}
