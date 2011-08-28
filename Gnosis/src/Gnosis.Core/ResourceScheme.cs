using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class ResourceScheme
        : IResourceScheme
    {
        private ResourceScheme(string name, string description)
            : this(name, description, true, true)
        {
        }

        private ResourceScheme(string name, string description, bool isOfficial)
            : this(name, description, isOfficial, true)
        {
        }

        private ResourceScheme(string name, string description, bool isOfficial, bool isRecognized)
        {
            this.name = name;
            this.description = description;
            this.isOfficial = isOfficial;
            this.isRecognized = isRecognized;
        }

        private readonly string name;
        private readonly string description;
        private readonly bool isOfficial;
        private readonly bool isRecognized;

        public string Name
        {
            get { return name; }
        }

        public string Description
        {
            get { return description; }
        }

        public bool IsOfficial
        {
            get { return isOfficial; }
        }

        public bool IsRecognized
        {
            get { return isRecognized; }
        }

        static ResourceScheme()
        {
            AddScheme(aaa);
            AddScheme(aaas);
            AddScheme(acap);
            AddScheme(cap);
            AddScheme(cid);
            AddScheme(crid);
            AddScheme(data);
            AddScheme(dav);
            AddScheme(dict);
            AddScheme(dns);
            AddScheme(fax);
            AddScheme(file);
            AddScheme(ftp);
            AddScheme(geo);
            AddScheme(go);
            AddScheme(gopher);
            AddScheme(h323);
            AddScheme(http);
            AddScheme(https);
            AddScheme(icap);
            AddScheme(im);
            AddScheme(imap);
            AddScheme(info);
            AddScheme(ipp);
            AddScheme(iris);
            AddScheme(iris_beep);
            AddScheme(iris_lws);
            AddScheme(iris_xpc);
            AddScheme(iris_xpcs);
            AddScheme(ldap);
            AddScheme(mailto);
            AddScheme(mid);
            AddScheme(modem);
            AddScheme(msrp);
            AddScheme(msrps);
            AddScheme(mtqp);
            AddScheme(mupdate);
            AddScheme(news);
            AddScheme(nfs);
            AddScheme(nntp);
            AddScheme(opaquelocktoken);
            AddScheme(pop);
            AddScheme(pres);
            AddScheme(propspero);
            AddScheme(rsync);
            AddScheme(rtsp);
            AddScheme(service);
            AddScheme(shttp);
            AddScheme(sieve);
            AddScheme(sip);
            AddScheme(sips);
            AddScheme(sms);
            AddScheme(snmp);
            AddScheme(soap_beep);
            AddScheme(soap_beeps);
            AddScheme(tag);
            AddScheme(tel);
            AddScheme(telnet);
            AddScheme(tftp);
            AddScheme(thismessage);
            AddScheme(tip);
            AddScheme(tv);
            AddScheme(urn);
            AddScheme(vemmi);
            AddScheme(wais);
            AddScheme(xmlrpc_beep);
            AddScheme(xmlrpc_beeps);
            AddScheme(xmpp);
            AddScheme(z39_50r);
            AddScheme(z39_50s);

            AddScheme(about);
            AddScheme(adiumxtra);
            AddScheme(afp);
            AddScheme(aim);
            AddScheme(apt);
            AddScheme(aw);
            AddScheme(bitcoin);
            AddScheme(bolo);
            AddScheme(callto);
            AddScheme(chrome);
            AddScheme(coap);
            AddScheme(content);
            AddScheme(cvs);
            AddScheme(doi);
        }

        private static void AddScheme(IResourceScheme scheme)
        {
            schemes.Add(scheme.Name, scheme);
        }

        private static readonly IDictionary<string, IResourceScheme> schemes = new Dictionary<string, IResourceScheme>();

        public static IEnumerable<IResourceScheme> Schemes
        {
            get { return schemes.Values; }
        }

        public static IResourceScheme Parse(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            var lower = name.ToLower();

            return schemes.ContainsKey(lower) ?
                schemes[lower]
                : new ResourceScheme(name, "Unrecognized Scheme", false, false);
        }

        #region Official Schemes

        /// <summary>
        /// Diameter Protocol
        /// </summary>
        /// <remarks>
        /// RFC 3588
        /// </remarks>
        /// <example>
        /// aaa://host.example.com:1813;transport=udp;protocol=radius
        /// </example>
        public static IResourceScheme aaa = new ResourceScheme("aaa", "Diameter Protocol");
        
        /// <summary>
        /// Secure equivalent of aaa
        /// </summary>
        /// <remarks>
        /// RFC 3588
        /// </remarks>
        /// <example>
        /// aaas://<host>[:<port>][;transport=<transport>][;protocol=<protocol>]
        /// </example>
        public static IResourceScheme aaas = new ResourceScheme("aaas", "Secure Diameter Protocol");

        /// <summary>
        /// Application Configuration Access Protocol	RFC 2244	acap://[<user>[;AUTH=<type>]@]<host>[:<port>]/<entry>	URL scheme used within the ACAP protocol for the "subdataset" attribute, referrals and inheritance
        /// </summary>
        public static IResourceScheme acap = new ResourceScheme("acap", "Application Configuration Access Protocol");

        /// <summary>
        /// Calendar access protocol	RFC 4324	generic syntax	URL scheme used to designate both calendar stores and calendars accessible using the CAP protocol
        /// </summary>
        public static IResourceScheme cap = new ResourceScheme("cap", "Calendar Access Protocol");

        /// <summary>
        /// Referencing individual parts of an SMTP/MIME message	RFC 2111 RFC 2392	cid:<content-id>	e.g. referencing an attached image within a formatted e-mail. (See also mid:)
        /// </summary>
        public static IResourceScheme cid = new ResourceScheme("cid", "Referencing Individual Parts of SMTP/MIME message");

        /// <summary>
        /// RFC 4078	crid://<host>/<data>	Allow references to scheduled publications of broadcast media content.
        /// </summary>
        public static IResourceScheme crid = new ResourceScheme("crid", "TV-Anytime Content Reference Identifier");
	
        /// <summary>
        /// RFC 2397	data:<mediatype>[;base64],<data>
        /// </summary>
        public static IResourceScheme data = new ResourceScheme("data", "Inclusion of small data items inline");

        /// <summary>
        /// Used for internal identifiers only; WebDAV itself addresses resources using the http: and https: schemes. [1]
        /// </summary>
        /// <remarks>
        /// RFC 2518
        /// RFC 4918
        /// </remarks>
        public static IResourceScheme dav = new ResourceScheme("dav", "HTTP Extensions for Distributed Authoring (WebDAV)");
        
        /// <summary>
        /// refer to definitions or word lists available using the DICT protocol
        /// </summary>
        /// <example>
        /// dict://<user>;<auth>@<host>:<port>/d:<word>:<database>:<n>
        /// dict://<user>;<auth>@<host>:<port>/m:<word>:<database>:<strat>:<n>
        /// </example>
        /// <remarks>
        /// RFC 2229
        /// </remarks>
        public static IResourceScheme dict = new ResourceScheme("dict", "Dictionary service protocol");
        
        /// <summary>
        /// designates a DNS resource record set, referenced by domain name, class, type, and, optionally, the authority
        /// </summary>
        /// <example>
        /// dns:[//<host>[:<port>]/]<dnsname>[?<dnsquery>]
        /// dns:example?TYPE=A;CLASS=IN
        /// dns://192.168.1.1/ftp.example.org?type=A
        /// </example>
        /// <remarks>
        /// RFC 4501
        /// </remarks>
        public static IResourceScheme dns = new ResourceScheme("dns", "Domain Name System");
        
        /// <summary>
        /// Telefacsimile Protocol
        /// </summary>
        /// <example>
        /// fax:[phonenumber]	Seems to be deprecated in RFC 3966 in favour of tel:
        /// </example>
        /// <remarks>
        /// RFC 2806
        /// RFC 3966
        /// </remarks>
        public static IResourceScheme fax = new ResourceScheme("fax", "Used for telefacsimile numbers");

        /// <summary>
        /// File Protocol
        /// </summary>
        /// <example>
        /// file://[host]/path
        /// file:[//host]/path
        /// </example>
        /// <remarks>
        /// RFC 1738: Since this usually used for local files the host from RFC 1738 is often empty leading to a starting triple /.
        /// RFC 3986: allows an absolute path with no host part.
        /// </remarks>
        public static IResourceScheme file = new ResourceScheme("file", "Addressing files on local or network file systems");
        	
        /// <summary>
        /// File Transfer Protocol
        /// </summary>
        /// <remarks>
        /// RFC 1738
        /// IETF Draft
        /// Old IETF Draft	
        /// </remarks>
        public static IResourceScheme ftp = new ResourceScheme("ftp", "FTP resources");
	
        /// <summary>
        /// Geographic Location Protocol
        /// </summary>
        /// <example>
        /// geo:<lat>,<lon>[,<alt>][;u=<uncertainty>] (for WGS-84)
        /// </example>
        /// <remarks>
        /// RFC 5870
        /// Other coordinate reference systems (including those for non-terrestrial globes, such as The Moon and Mars) will be supported, once registered.
        /// </remarks>
        public static IResourceScheme geo = new ResourceScheme("geo", "A Uniform Resource Identifier for Geographic Locations");

        /// <summary>
        /// Common Name Resolution Protocol
        /// </summary>
        /// <example>
        /// go://[<host>]?[<common-name>]*[;<attribute>=[<type>,]<value>]
        /// go:<common-name>*[;<attribute>=[(type),](value)]
        /// </example>
        /// <remarks>
        /// RFC 3368
        /// </remarks>
        public static IResourceScheme go = new ResourceScheme("go", "Common Name Resolution Protocol");
        	
        /// <summary>
        /// Gopher Protocol
        /// </summary>
        /// <example>
        /// gopher://[host]:[port]/[item type]/[path]
        /// </example>
        /// <remarks>
        /// RFC 4266
        /// </remarks>
        public static IResourceScheme gopher = new ResourceScheme("gopher", "Gopher protocol");
		
        /// <summary>
        /// H.323 multimedia communications protocol
        /// </summary>
        /// <example>
        /// h323:[<user>@]<host>[:<port>][;<parameters>]
        /// </example>
        /// <remarks>
        /// RFC 3508
        /// </remarks>
        public static IResourceScheme h323 = new ResourceScheme("h323", "H.323 multimedia communications");
        
        /// <summary>
        /// Hypertext Transfer Protocol
        /// </summary>
        /// <remarks>
        /// RFC 1738
        /// RFC 2616 (makes RFC 2068 obsolete)
        /// </remarks>
        public static IResourceScheme http = new ResourceScheme("http", "Hypertext Transfer Protocol");

        /// <summary>
        /// Secure Hypertext Transfer Protocol
        /// </summary>
        /// <remarks>
        /// RFC 2817
        /// </remarks>
        public static IResourceScheme https = new ResourceScheme("https", "Hypertext Transfer Protocol Secure");
        
        /// <summary>
        /// Inter-Asterisk Exchange Protocol Version 2
        /// </summary>
        /// <example>
        /// iax:[<username>@]<host>[:<port>][/<number>[?<context>]]
        /// iax:[2001:db8::1]:4569/alice?friends
        /// iax:johnQ@example.com/12022561414
        /// </example>
        /// <remarks>
        /// RFC 5456
        /// </remarks>
        public static IResourceScheme iax = new ResourceScheme("iax", "Inter-Asterisk eXchange protocol version 2");

        /// <summary>
        /// Internet Content Adaptation Protocol
        /// </summary>
        /// <remarks>
        /// RFC 3507
        /// </remarks>
        public static IResourceScheme icap = new ResourceScheme("icap", "Internet Content Adaptation Protocol");
        
        /// <summary>
        /// Instant Messaging Protocol
        /// </summary>
        /// <example>
        /// im:<username>@<host>
        /// </example>
        /// <remarks>
        /// RFC 3860
        /// Works as xmpp: URI for single user chat sessions.
        /// </remarks>
        public static IResourceScheme im = new ResourceScheme("im", "Instant messaging protocol");
        
        /// <summary>
        /// Internet Message Access Protocol
        /// </summary>
        /// <example>
        /// imap://[<user>[;AUTH=<type>]@]<host>[:<port>]/<command>
        /// </example>
        /// <remarks>
        /// RFC 2192
        /// RFC 5092
        /// </remarks>
        public static IResourceScheme imap = new ResourceScheme("imap", "Internet Message Access Protocol");
            
        /// <summary>
        /// Information Protocol
        /// </summary>
        /// <remarks>
        /// RFC 4452
        /// Information Assets with Identifiers in Public Namespaces
        /// </remarks>
        public static IResourceScheme info = new ResourceScheme("info", "Information Protocol");
		
        /// <summary>
        /// Internet Printing Protocol
        /// </summary>
        /// <remarks>
        /// RFC 3510
        /// </remarks>
        public static IResourceScheme ipp = new ResourceScheme("ipp", "Internet Printing Protocol");		
        
        /// <summary>
        /// Internet Registry Information Service
        /// </summary>
        /// <remarks>
        /// RFC 3981
        /// </remarks>
        public static IResourceScheme iris = new ResourceScheme("iris", "Internet Registry Information Service");
        
        /// <summary>
        /// Internet Registry Information Service (Beep)
        /// </summary>
        /// <remarks>
        /// RFC 3983
        /// </remarks>
        public static IResourceScheme iris_beep = new ResourceScheme("iris.beep", "Internet Registry Information Service (Beep)");
        
        /// <summary>
        /// Internet Registry Information Service (XPC)
        /// </summary>
        /// <remarks>
        /// RFC 4992
        /// </remarks>
        public static IResourceScheme iris_xpc = new ResourceScheme("iris.xpc", "Internet Registry Information Service (XPC)");
        
        /// <summary>
        /// Internet Registry Information Service (XPCS)
        /// </summary>
        /// <remarks>
        /// RFC 4992
        /// </remarks>
        public static IResourceScheme iris_xpcs = new ResourceScheme("iris.xpcs", "Internet Registry Information Service (XPCS)");
        
        /// <summary>
        /// Internet Registry Information Service (LWS)
        /// </summary>
        /// <remarks>
        /// RFC 4993
        /// </remarks>
        public static IResourceScheme iris_lws = new ResourceScheme("iris.lws", "Internet Registry Information Service (LWS)");
                
        /// <summary>
        /// Lightweight Directory Access Protocol
        /// </summary>
        /// <example>
        /// ldap://[<host>[:<port>]][/<dn> [?[<attributes>][?[<scope>][?[<filter>][?<extensions>]]]]]
        /// ldap://ldap1.example.net:6666/o=University%20of%20Michigan, c=US??sub?(cn=Babs%20Jensen)
        /// </example>
        /// <remarks>
        /// RFC 2255
        /// RFC 4516
        /// </remarks>
        public static IResourceScheme ldap = new ResourceScheme("ldap", "Lightweight Directory Access Protocol");
            
        /// <summary>
        /// Mail To Protocol
        /// </summary>
        /// <example>
        /// mailto:<address>[?<header1>=<value1>[&<header2>=<value2>]]
        /// mailto:jsmith@example.com?subject=A%20Test&body=My%20idea%20is%3A%20%0A
        /// </example>
        /// <remarks>
        /// RFC 1738
        /// RFC 2368
        /// IETF Draft
        /// Headers are optional, but often include subject=; body= can be used to pre-fill the body of the message.
        /// </remarks>
        public static IResourceScheme mailto = new ResourceScheme("mailto", "Mail To Protocol");
        
        /// <summary>
        /// SMPT Message Reference Protocol
        /// </summary>
        /// <example>
        /// mid:<message-id>[/<content-id>]	(See also cid:)
        /// </example>
        /// <remarks>
        /// RFC 2111
        /// RFC 2392
        /// </remarks>
        public static IResourceScheme mid = new ResourceScheme("mid", "Referencing SMTP/MIME messages, or parts of messages.");
        
        /// <summary>
        /// Modem Protocol
        /// </summary>
        /// <remarks>
        /// RFC 2806 (Seems to be deprecated in RFC 3966 in favour of tel:)
        /// </remarks>
        public static IResourceScheme modem = new ResourceScheme("modem", "Modem Protocol");
        
        /// <summary>
        /// Message Session Relay Protocol
        /// </summary>
        /// <remarks>
        /// RFC 4975
        /// </remarks>
        public static IResourceScheme msrp = new ResourceScheme("msrp", "Message Session Relay Protocol");

        /// <summary>
        /// Secure Message Session Relay Protocol
        /// </summary>
        /// <remarks>
        /// RFC 4975
        /// </remarks>
        public static IResourceScheme msrps = new ResourceScheme("msrps", "Secure Message Session Relay Protocol");

        /// <summary>
        /// Message Tracking Query Protocol
        /// </summary>
        /// <remarks>
        /// RFC 3887
        /// </remarks>
        public static IResourceScheme mtqp = new ResourceScheme("mtqp", "Message Tracking Query Protocol");

        /// <summary>
        /// Mailbox Update Protocol
        /// </summary>
        /// <remarks>
        /// RFC 3656
        /// </remarks>
        public static IResourceScheme mupdate = new ResourceScheme("mupdate", "Mailbox Update Protocol");
        

        /// <summary>
        /// Usenet News Protocol
        /// </summary>
        /// <example>
        /// news:<newsgroupname>
        /// news:<message-id>	References a particular resource, regardless of location.
        /// </example>
        /// <remarks>
        /// RFC 1738
        /// RFC 5538
        /// </remarks>
        public static IResourceScheme news = new ResourceScheme("news", "Usenet News Protocol");

        /// <summary>
        /// Network File System Resources
        /// </summary>
        public static IResourceScheme nfs = new ResourceScheme("nfs", "Network File System resources");
        
        /// <summary>
        /// Network News Protocol
        /// </summary>
        /// <example>
        /// nntp://<host>:<port>/<newsgroup-name>/<article-number>
        /// </example>
        /// <remarks>
        /// RFC 1738
        /// RFC 5538
        /// Referencing a specific host is often less useful than referencing the resource generically, as NNTP servers are not always publicly accessible
        /// </remarks>
        public static IResourceScheme nntp = new ResourceScheme("nntp", "Network News Protocol");
        
        /// <summary>
        /// Opaque Lock Token Protocol
        /// </summary>
        /// <remarks>
        /// RFC 2518
        /// RFC 4918
        /// </remarks>
        public static IResourceScheme opaquelocktoken = new ResourceScheme("opaquelocktoken", "Opaque Lock Token Protocol");
        
		/// <summary>
		/// Post Office Protocol version 3
		/// </summary>
        /// <example>
        /// pop://[<user>[;AUTH=<auth>]@]<host>[:<port>]	
        /// </example>
        /// <remarks>
        /// RFC 2384
        /// </remarks>
        public static IResourceScheme pop = new ResourceScheme("pop", "Accessing mailbox through POP3");
            
        /// <summary>
        /// Common Profile for Presence
        /// </summary>
        /// <example>
        /// pres:<address>[?<header1>=<value1>[&<header2>=<value2>]]	
        /// Similar to "mailto:"
        /// </example>
        /// <remarks>
        /// RFC 3859
        /// </remarks>
        public static IResourceScheme pres = new ResourceScheme("pres", "Common Profile for Presence (CPP)");
            
        /// <summary>
        /// Prospero Directory Service
        /// </summary>
        /// <remarks>
        /// RFC 1738
        /// RFC 4157
        /// Listed as "Historical" by IANA.
        /// </remarks>
        public static IResourceScheme propspero = new ResourceScheme("prospero", "Prospero Directory Service");
            
        /// <summary>
        /// Remote Synchronization Protocol
        /// </summary>
        /// <example>
        /// rsync://<host>[:<port>]/<path>
        /// </example>
        /// <remarks>
        /// RFC 5781
        /// </remarks>
        public static IResourceScheme rsync = new ResourceScheme("rsync", "Remote Synchronization Protocol");
            
        /// <summary>
        /// Real Time Streaming Protocol
        /// </summary>
        /// <remarks>
        /// RFC 2326
        /// </remarks>
        public static IResourceScheme rtsp = new ResourceScheme("rtsp", "Real Time Streaming Protocol");
        
        /// <summary>
        /// Service Protocol
        /// </summary>
        /// <remarks>
        /// RFC 2609
        /// </remarks>
        public static IResourceScheme service = new ResourceScheme("service", "Service Protocol");
        
        /// <summary>
        /// Secure Hypertext Transfer Protocol"
        /// </summary>
        /// <remarks>
        /// RFC 2660
        /// Largely superseded by HTTPS.
        /// </remarks>
        public static IResourceScheme shttp = new ResourceScheme("shttp", "Secure Hypertext Transfer Protocol");

        /// <summary>
        /// Manage Sieve Protocol
        /// </summary>
        /// <remarks>
        /// RFC 5804
        /// </remarks>
        public static IResourceScheme sieve = new ResourceScheme("sieve", "ManageSieve protocol");
		
        /// <summary>
        /// Session Initiation Protocol
        /// </summary>
        /// <example>
        /// sip:<user>[:<password>]@<host>[:<port>][;<uri-parameters>][?<headers>]
        /// sip:alice@atlanta.com?subject=project%20x&priority=urgent
        /// sip:+1-212-555-1212:1234@gateway.com;user=phone
        /// </example>
        /// <remarks>
        /// RFC 2543
        /// RFC 3969
        /// RFC 3261
        /// </remarks>
        public static IResourceScheme sip = new ResourceScheme("sip", "Session Initiation Protocol");
        
        /// <summary>
        /// Secure Session Initiation Protocol
        /// </summary>
        /// <example>
        /// RFC 3261	sips:<user>[:<password>]@<host>[:<port>][;<uri-parameters>][?<headers>]	
        /// </example>
        /// <remarks>
        /// RFC 3261
        /// RFC 3969
        /// </remarks>
        public static IResourceScheme sips = new ResourceScheme("sips", "Secure Session Initiation Protocol");
            
        /// <summary>
        /// Short Message Service Protocol
        /// </summary>
        /// <example>
        /// RFC 5724	sms:<phone number>?<action>
        /// sms:+15105550101?body=hello%20there
        /// sms:+15105550101,+15105550102?body=hello%20there
        /// </example>
        /// <remarks>
        /// RFC 5724
        /// Should be used as a subset to the tel: schema.[citation needed]
        /// </remarks>
        public static IResourceScheme sms = new ResourceScheme("sms", "Short Message Service Protocol");
        
        /// <summary>
        /// Simple Network Management Protocol
        /// </summary>
        /// <example>
        /// snmp://[user@]host[:port][/[<context>[;<contextEngineID>]][/<oid>]]
        /// snmp://example.com//1.3.6.1.2.1.1.3+
        /// snmp://tester5@example.com:8161/bridge1;800002b804616263
        /// </example>
        /// <remarks>
        /// RFC 4088	
        /// </remarks>
        public static IResourceScheme snmp = new ResourceScheme("snmp", "Simple Network Management Protocol");

        /// <summary>
        /// Simple Object Access Protocol (Beep)
        /// </summary>
        /// <remarks>
        /// RFC 3288
        /// </remarks>
        public static IResourceScheme soap_beep = new ResourceScheme("soap.beep", "Simple Object Access Protocol (Beep)");

        /// <summary>
        /// Simple Object Access Protocol (Secure Beep)
        /// </summary>
        /// <remarks>
        /// RFC 4227
        /// </remarks>
        public static IResourceScheme soap_beeps = new ResourceScheme("soap.beeps", "Simple Object Access Protocol (Secure Beep)");
            
        /// <summary>
        /// Tag Protocol
        /// </summary>
        /// <example>
        /// tag:<email/domainname>,<date>:<Item>
        /// </example>
        /// <remarks>
        /// RFC 4151
        /// Represented entities do not necessarily have to be accessible electronically.
        /// </remarks>
        public static IResourceScheme tag = new ResourceScheme("tag", "Tag Protocol");
        
        /// <summary>
        /// Telephone Number Protocol
        /// </summary>
        /// <example>
        /// tel:<phonenumber>
        /// </example>
        /// <remarks>
        /// RFC 5341
        /// RFC 3966
        /// RFC 2806
        /// </remarks>
        public static IResourceScheme tel = new ResourceScheme("tel", "Telephone Number Protocol");

        /// <summary>
        /// Telnet Protocol
        /// </summary>
        /// <example>
        /// telnet://<user>:<password>@<host>[:<port>/]	
        /// </example>
        /// <remarks>
        /// RFC 1738
        /// RFC 4248
        /// </remarks>
        public static IResourceScheme telnet = new ResourceScheme("telnet", "Telnet Protocol");
        
        /// <summary>
        /// Trivial File Transfer Protocol
        /// </summary>
        /// <remarks>
        /// RFC 3617
        /// </remarks>
        public static IResourceScheme tftp = new ResourceScheme("tftp", "Trivial File Transfer Protocol");

        /// <summary>
        /// Multipart-Related Protocol
        /// </summary>
        /// <remarks>
        /// RFC 2557
        /// </remarks>
        public static IResourceScheme thismessage = new ResourceScheme("thismessage", "Multipart Related Protocol");
        
        /// <summary>
        /// Transaction Internet Protocol
        /// </summary>
        /// <remarks>
        /// RFC 2371
        /// </remarks>
        public static IResourceScheme tip = new ResourceScheme("tip", "Transaction Internet Protocol");
        
        /// <summary>
        /// TV Broadcast Protocol
        /// </summary>
        /// <remarks>
        /// RFC 2838
        /// </remarks>
        public static IResourceScheme tv = new ResourceScheme("tv", "TV Broadcast Protocol");
        
        /// <summary>
        /// Uniform Resource Name
        /// </summary>
        /// <example>
        /// urn:<namespace>:<specificpart>
        /// </example>
        /// <remarks>
        /// RFC 2141
        /// </remarks>
        public static IResourceScheme urn = new ResourceScheme("urn", "Uniform Resource Name");
        
        /// <summary>
        /// Versatile Multimedia Interface
        /// </summary>
        /// <remarks>
        /// RFC 2122
        /// </remarks>
        public static IResourceScheme vemmi = new ResourceScheme("vemmi", "Versatile Multimedia Interface");
		
        /// <summary>
        /// Wide Area Information Server Protocol
        /// </summary>
        /// <example>
        /// wais://<host>:<port>/<database>[?<search>] or wais://<host>:<port>/<database>/<wtype>/<wpath>	
        /// </example>
        /// <remarks>
        /// RFC 1738
        /// RFC 4156
        /// Listed as "Historical" by IANA.
        /// </remarks>
        public static IResourceScheme wais = new ResourceScheme("wais", "Wide Area Information Server Protocol");	
        
        /// <summary>
        /// XML Remote Procedure Call Beep Protocol
        /// </summary>
        /// <remarks>
        /// RFC 3529
        /// </remarks>
        public static IResourceScheme xmlrpc_beep = new ResourceScheme("xmlrpc.beep", "XML Remote Procedure Call Beep Protocol");

        /// <summary>
        /// XML Remote Procedure Call Secure Beep Protocol
        /// </summary>
        /// <remarks>
        /// RFC 3529
        /// </remarks>
        public static IResourceScheme xmlrpc_beeps = new ResourceScheme("xmlrpc.beeps", "XML Remote Procedure Call Secure Beep Protocol");
        
        /// <summary>
        /// Extensible Messaging and Presence Protocol
        /// </summary>
        /// <example>
        /// xmpp:<user>@<host>[:<port>]/[<resource>][?<query>]
        /// </example>
        /// <remarks>
        /// RFC 4622
        /// RFC 5122
        /// </remarks>
        public static IResourceScheme xmpp = new ResourceScheme("xmpp", "Extensible Messaging and Presence Protocol");

        /// <summary>
        /// Z39.40 Retrieval Protocol
        /// </summary>
        /// <example>
        /// z39.50r://<host>[:<port>]/<database>?<docid>[;esn=<elementset>][;rs=<recordsyntax>]
        /// </example>
        /// <remarks>
        /// RFC 2056
        /// </remarks>
        public static IResourceScheme z39_50r = new ResourceScheme("z39.50r", "Z39.50 Retrieval Protocol");
        
        /// <summary>
        /// Z39.50 Session Protocol
        /// </summary>
        /// <example>
        /// z39.50s://<host>[:<port>]/[<database>][?<docid>][;esn=<elementset>][;rs=<recordsyntax>]
        /// </example>
        /// <remarks>
        /// RFC 2056
        /// </remarks>
        public static IResourceScheme z39_50s = new ResourceScheme("z39.50s", "Z39.50 Session Protocol");

        #endregion

        #region Unofficial Schemes

        /// <summary>
        /// About Protocol
        /// </summary>
        /// <remarks>
        /// IETF Draft	See about URI scheme for more details.	
        /// Widely used by web browsers, sometimes even providing interactive resources. The Opera web browser uses opera: instead.
        /// </remarks>
        public static IResourceScheme about = new ResourceScheme("about", "About Protocol", false);

        /// <summary>
        /// Adium Xtra Installation Protocol
        /// </summary>
        /// <example>
        /// adiumxtra://www.adiumxtras.com/download/(xtra number)
        /// </example>
        /// <remarks>
        /// Displaying product information and internal information
        /// </remarks>
        public static IResourceScheme adiumxtra = new ResourceScheme("adiumxtra", "Adium Xtra Installation Protocol", false);

        /// <summary>
        /// Apple Filing Protocol
        /// </summary>
        /// <example>
        /// afp://[<user>@]<host>[:<port>][/[<path>]]
        /// </example>
        /// <remarks>
        /// IETF Draft
        /// </remarks>
        public static IResourceScheme afp = new ResourceScheme("afp", "Apple Filing Protocol", false);

        /// <summary>
        /// America Online Instant Messenger Protocol
        /// </summary>
        /// <example>
        /// aim:<function>?<parameters>	Functions include goim, addbuddy, and buddyicon.
        /// </example>
        public static IResourceScheme aim = new ResourceScheme("aim", "America Online Instant Messenger Protocol", false);
        
        /// <summary>
        /// Advanced Packaging Tool Protocol
        /// </summary>
        /// <example>
        /// apt:<package name>
        /// </example>
        /// <remarks>
        /// Commonly found on websites which contain Debian software repositories.
        /// </remarks>
        public static IResourceScheme apt = new ResourceScheme("apt", "Advanced Packaging Tool Protocol", false);
        
        /// <summary>
        /// Active Worlds Protocol
        /// </summary>
        /// <example>
        /// aw://<worldserver host>:<worldserver port>/<worldname>
        /// </example>
        /// <remarks>
        /// Mostly found in HTTP referers when users open a website from within a Active Worlds world.
        /// </remarks>
        public static IResourceScheme aw = new ResourceScheme("aw", "Active Worlds Protocol", false);
        
        /// <summary>
        /// Bitcoin Protocol
        /// </summary>
        /// <example>
        /// bitcoin:<address>[?[amount=<size>][&][label=<label>][&][message=<message>]]
        /// </example>
        public static IResourceScheme bitcoin = new ResourceScheme("bitcoin", "Bitcoin Protocol", false);

        /// <summary>
        /// Bolo Protocol
        /// </summary>
        /// <example>
        /// bolo://<hostname>/
        /// </example>
        /// <remarks>
        /// Mostly passed via IRC or via tracker servers.
        /// </remarks>
        public static IResourceScheme bolo = new ResourceScheme("bolo", "Bolo Protocol", false);

        /// <summary>
        /// Call-To Protocol
        /// </summary>
        /// <example>
        /// callto:(screenname)
        /// callto:(phonenumber)
        /// </example>
        /// <remarks>
        /// Launching Skype call (+And in Hungary the KLIP Software call too) (unofficial; see also skype:)
        /// Introduced with Microsoft NetMeeting. Works with current version of Skype with Firefox, Internet Explorer and Safari
        /// </remarks>
        public static IResourceScheme callto = new ResourceScheme("callto", "Call-To Protocol", false);
        
        /// <summary>
        /// Mozilla Chrome Protocol
        /// </summary>
        /// <example>
        /// chrome://<package>/<section>/<path> (Where <section> is either "content", "skin" or "locale")
        /// </example>
        /// <remarks>
        /// Specifies user interfaces built using XUL in Mozilla-based browsers.
        /// Works only in Mozilla-based browsers such as Firefox, SeaMonkey and Netscape. Not related to the Google Chrome browser.
        /// </remarks>
        public static IResourceScheme chrome = new ResourceScheme("chrome", "Mozilla Chrome Protocol", false);

        /// <summary>
        /// Constrained Application Protocol
        /// </summary>
        /// <example>
        /// coap://<host>[:<port>]/<path>[?<query>]
        /// </example>
        /// <remarks>
        /// IETF Draft
        /// Identify CoAP resources and provide a means of locating the resource.
        /// </remarks>
        public static IResourceScheme coap = new ResourceScheme("coap", "Constrained Application Protocol", false);
        
        /// <summary>
        /// Android Content Provider Protocol
        /// </summary>
        /// <example>
        /// content://provider/<path>
        /// </example>
        /// <remarks>
        /// Open Handset Alliance
        /// Accessing an Android content provider.
        /// </remarks>
        public static IResourceScheme content = new ResourceScheme("content", "Android Content Provider Protocol", false);

        /// <summary>
        /// Concurrent Versions System Protocol
        /// </summary>
        /// <example>
        /// cvs://<method:logindetails>@<repository>/<modulepath>;[date=date to retrieve | tag=tag to retrieve]	
        /// </example>
        /// <remarks>
        /// Concurrent Versions System
        /// Provides a link to a Concurrent Versions System (CVS) Repository
        /// </remarks>
        public static IResourceScheme cvs = new ResourceScheme("cvs", "Concurrent Versions System Protocol", false);

        /// <summary>
        /// Digital Object Identifier Protocol
        /// </summary>
        /// <example>
        /// doi:10.<publisher number>/<suffix>
        /// doi:10.1000/182
        /// </example>
        /// <remarks>
        /// IETF Draft
        /// a digital identifier for any object of intellectual property.
        /// Used e.g. for most scientific publications.
        /// Can be resolved via HTTP (transormed into an URL) by prepending http://dx.doi.org/ or http://hdl.handle.net/ in front.
        /// </remarks>
        public static IResourceScheme doi = new ResourceScheme("doi", "Digital Object Identifier Protocol", false);

        /// <summary>
        /// eDonkey2000 Protocol
        /// </summary>
        /// <example>
        /// ed2k://|file|<filename>|<size of file>|<hash of file>|/
        /// ed2k://|server|<host>|<port>|/
        /// </example>
        /// <remarks>
        /// Links to servers are also possible, as are additional parameters. Official documentation from eDonkey2000 website at the Wayback Machine.
        /// </remarks>
        public static IResourceScheme ed2k = new ResourceScheme("ed2k", "eDonkey2000 Protocol", false);

        /// <summary>
        /// Apple Facetime Protocol
        /// </summary>
        /// <example>
        /// facetime://<address>|<MSISDN>|<mobile number>
        /// facetime://+19995551234
        /// </example>
        /// <remarks>
        /// Apple Inc.
        /// FaceTime is a video conferencing software developed by Apple for iPhone 4, the fourth generation iPod Touch, and computers running Mac OS X.
        /// Apple has not published documentation on this protocol yet.
        /// </remarks>
        public static IResourceScheme facetime = new ResourceScheme("facetime", "Apple Facetime Protocol", false);

        /// <summary>
        /// RSS Feed Protocol
        /// </summary>
        /// <example>
        /// feed:<absolute_uri>
        /// feed://<hierarchical part>
        /// feed://example.com/rss.xml
        /// feed:https://example.com/rss.xml
        /// </example>
        /// <remarks>
        /// See Feed URI scheme for a detailed overview of common implementations, supported software, and critics.
        /// </remarks>
        public static IResourceScheme feed = new ResourceScheme("feed", "RSS Feed Protocol", false);
        
        /// <summary>
        /// Finger Protocol
        /// </summary>
        /// <example>
        /// finger://host[:port][/<request>]	
        /// </example>
        /// <remarks>
        /// IETF Draft
        /// </remarks>
        public static IResourceScheme finger = new ResourceScheme("finger", "Finger Protocol", false);
        
        /// <summary>
        /// File Transfer Over SSH Protocol
        /// </summary>
        /// <example>
        /// fish://[<username>[:<password>]@]<hostname>[:<port>]
        /// </example>
        /// <remarks>
        /// Accessing another computer's files using the SSH protocol	fish KDE kioslave		
        /// See Files transferred over shell protocol for details about the protocol.
        /// </remarks>
        public static IResourceScheme fish = new ResourceScheme("fish", "File Transfer Over SSH Protocol", false);
        
        /// <summary>
        /// Git Protocol
        /// </summary>
        /// <example>
        /// git://github.com/user/project-name.git	
        /// </example>
        /// <remarks>
        /// Git
        /// Provides a link to a GIT repository
        /// </remarks>
        public static IResourceScheme git = new ResourceScheme("git", "Git Protocol", false);

        /// <summary>
        /// Gadu-Gadu Protocol
        /// </summary>
        /// <example>
        /// gg:<userid>	
        /// </example>
        /// <remarks>
        /// Starting chat with Gadu-Gadu user
        /// </remarks>
        public static IResourceScheme gg = new ResourceScheme("gg", "Gadu-Gadu Protocol", false);

        /// <summary>
        /// Gizmo5 Protocol
        /// </summary>
        /// <example>
        /// gizmoproject://call?id=<gizmo_id>
        /// </example>
        /// <remarks>
        /// May use sip:// instead of gizmoproject:// in recent versions of Gizmo5.
        /// </remarks>
        public static IResourceScheme gizmoproject = new ResourceScheme("gizmoproject", "Gizmo5 Protocol", false);

        /// <summary>
        /// Google Talk Protocol
        /// </summary>
        /// <example>
        /// gtalk:chat?jid=example@gmail.com
        /// </example>
        /// <remarks>
        /// Start a chat with a Google Talk user
        /// See Google Talk, XMPP, and http://juberti.blogspot.com/2006/11/gtalk-uri.html for more information
        /// </remarks>
        public static IResourceScheme gtalk = new ResourceScheme("gtalk", "Google Talk Protocol", false);

        /// <summary>
        /// Internet Relay Chat Protocol
        /// </summary>
        /// <example>
        /// irc://<host>[:<port>]/[<channel>[?<password>]]
        /// Assuming the client knows a server associated with the name, "host" may optionally be an IRC network name.
        /// </example>
        /// <remarks>
        /// IETF Draft
        /// Connecting to an Internet Relay Chat server to join a channel.
        /// </remarks>
        public static IResourceScheme irc = new ResourceScheme("irc", "Internet Relay Chat Protocol", false);

        /// <summary>
        /// Secure Internet Relay Chat Protocol
        /// </summary>
        /// <example>
        /// ircs://<host>[:<port>]/[<channel>[?<password>]]
        /// </example>
        /// <remarks>
        /// IETF Draft
        /// </remarks>
        public static IResourceScheme ircs = new ResourceScheme("ircs", "Secure Internet Relay Chat Protocol", false);

        /// <summary>
        /// Internet Relay Chat Protocol for IPv6
        /// </summary>
        /// <example>
        /// irc6://<host>[:<port>]/[<channel>[?<password>]]
        /// </example>
        /// <remarks>
        /// See irc
        /// </remarks>
        public static IResourceScheme irc6 = new ResourceScheme("irc6", "Internet Relay Chat Protocol for IPv6", false);

        /// <summary>
        /// iTunes Music Store Protocol
        /// </summary>
        /// <example>
        /// itms://itunes.com/(path)
        /// </example>
        /// <remarks>
        /// Apple Inc
        /// </remarks>
        public static IResourceScheme itms = new ResourceScheme("itms", "iTunes Music Store Protocol", false);

        /// <summary>
        /// iTunes Application Store Protocol
        /// </summary>
        /// <example>
        /// itms-apps://itunes.com/apps/(path)
        /// </example>
        /// <remarks>
        /// Apple Inc
        /// </remarks>
        public static IResourceScheme itms_apps = new ResourceScheme("itms-apps", "iTunes Application Store Protocol", false);

        /// <summary>
        /// Java Archive Protocol
        /// </summary>
        /// <example>
        /// jar:<url>!/[<entry>]
        /// </example>
        /// <remarks>
        /// Java API		
        /// Works for any ZIP based file.
        /// </remarks>
        public static IResourceScheme jar = new ResourceScheme("jar", "Java Archive Protocol", false);

        /// <summary>
        /// Javascript Protocol
        /// </summary>
        /// <example>
        /// javascript:<javascript to execute>
        /// </example>
        /// <remarks>
        /// IETF Draft
        /// Works in any modern browser.
        /// </remarks>
        public static IResourceScheme javascript = new ResourceScheme("javascript", "Javascript Protocol", false);

        /// <summary>
        /// Keyparc Protocol
        /// </summary>
        /// <example>
        /// </example>
        /// keyparc://encrypt/<username>/<uri>
        /// keyparc://decrypt/<username>/<uri>
        /// <remarks>
        /// Bloombase
        /// Keyparc encrypt/decrypt resource.
        /// </remarks>
        public static IResourceScheme keyparc = new ResourceScheme("keyparc", "Keyparc Protocol", false);

        /// <summary>
        /// Last.fm Stream Protocol
        /// </summary>
        /// <example>
        /// lastfm://<radio_stream>
        /// lastfm://globaltags/<genre>
        /// lastfm://user/<username>/<stuff>
        /// </example>
        /// <remarks>
        /// Connecting to a radio stream from Last.fm.
        /// </remarks>
        public static IResourceScheme lastfm = new ResourceScheme("lastfm", "Last.fm Stream Protocol", false);

        /// <summary>
        /// Secure Lightweight Directory Access Protocol
        /// </summary>
        /// <example>
        /// ldaps://[<host>[:<port>]][/<dn> [?[<attributes>][?[<scope>][?[<filter>][?<extensions>]]]]]
        /// </example>
        /// <remarks>
        /// IETF Draft
        /// Not an IETF standard, but commonly used in applications.
        /// </remarks>
        public static IResourceScheme ldaps = new ResourceScheme("ldaps", "Secure Lightweight Directory Access Protocol", false);

        /// <summary>
        /// Magnet Protocol
        /// </summary>
        /// <example>
        /// magnet:?xt=urn:sha1:<hash of file>&dn=<display name>
        /// (other parameters are also possible)
        /// </example>
        /// <remarks>
        /// Used by various peer-to-peer clients, usually providing the hash of a file to be located on the network.
        /// </remarks>
        public static IResourceScheme magnet = new ResourceScheme("magnet", "Magnet Protocol", false);

        /// <summary>
        /// Mobile Map Protocol
        /// </summary>
        /// <example>
        /// maps:q=<physical location>
        /// </example>
        /// <remarks>
        /// Some mobile web browsers will launch a dedicated mapping application. See also "geo:" (RFC 5870)
        /// </remarks>
        public static IResourceScheme maps = new ResourceScheme("maps", "Mobile Map Protocol", false);

        /// <summary>
        /// Android Market Protocol
        /// </summary>
        /// <example>
        /// market://details?id=Package_name
        /// market://search?q=Search_Query
        /// market://search?q=pub:Publisher_Name
        /// </example>
        /// <remarks>
        /// Android
        /// </remarks>
        public static IResourceScheme market = new ResourceScheme("market", "Android Market Protocol", false);
        
        /// <summary>
        /// Apple Message Protocol
        /// </summary>
        /// <example>
        /// message:<MESSAGE-ID>
        /// message://<MESSAGE-ID>
        /// </example>
        /// <remarks>
        /// Apple Inc
        /// Supported by Mail since OS X 10.5
        /// </remarks>
        public static IResourceScheme message = new ResourceScheme("message", "Apple Message Protocol", false);
        
        /// <summary>
        /// Windows Streaming Media Protocol
        /// </summary>
        /// <example>
        /// mms://<host>:<port>/<path>
        /// </example>
        /// <remarks>
        /// Used by Windows Media Player to stream audio and/or video.
        /// </remarks>
        public static IResourceScheme mms = new ResourceScheme("mms", "Windows streaming media Protocol", false);

        /// <summary>
        /// Windows Live Messenger Protocol
        /// </summary>
        /// <example>
        /// msnim:add?contact=nada@example.com     Add a contact to the buddy list
        /// msnim:chat?contact=nada@example.com    Start a conversation with a contact
        /// msnim:voice?contact=nada@example.com   Start a voice conversation with a contact
        /// msnim:video?contact=nada@example.com   Start a video conversation with a contact
        /// For web pages use this HTML: &lt;a href="chat?contact=nada@example.com"&gt;Click to chat!&lt;/a&gt;
        /// </example>
        /// <remarks>
        /// Can be invoked from a web page or via a run command or an Internet Explorer browser URL (won't work with Firefox 2.0.0.8). 
        /// </remarks>
        public static IResourceScheme msnim = new ResourceScheme("msnim", "Windows Live Messenger Protocol", false);


        /// <summary>
        /// Mumble Protocol
        /// </summary>
        /// <example>
        /// mumble://[username[:password]@]<address>[:port]/[channelpath]?version=<serverversion>[&title=<servername>][&url=<serverurl>]	
        /// </example>
        /// <remarks>
        /// Official documentation from Mumble website
        /// </remarks>
        public static IResourceScheme mumble = new ResourceScheme("mumble", "Mumble Protocol", false);

        /// <summary>
        /// Apache Maven Protocol
        /// </summary>
        /// <example>
        /// mvn:org.ops4j.pax.web.bundles/service/0.2.0-SNAPSHOT
        /// mvn:http://user:password@repository.ops4j.org/maven2!org.ops4j.pax.web.bundles/service/0.2.0
        /// </example>
        /// <remarks>
        /// Access Apache Maven repository artifacts	
        /// </remarks>
        public static IResourceScheme mvn = new ResourceScheme("mvn", "Apache Maven Protocol", false);
        
    	/// <summary>
    	/// Lotus Notes Protocol
    	/// </summary>
        /// <example>
        /// notes://<address>
        /// </example>
        /// <remarks>
        /// Used by IBM Lotus Notes to refer to documents and databases stored within the Lotus Notes system. When clicked in a browser on a computer with Lotus Notes client installed, Notes will open the document link as if a Notes DocLink were clicked within Notes.
        /// </remarks>
        public static IResourceScheme notes = new ResourceScheme("notes", "Lotus Notes Protocol", false);

        /// <summary>
        /// Palm WebOS System Service Protocol
        /// </summary>
        /// <example>
        /// palm:<servicename>[/<method>]]/
        /// </example>
        /// <remarks>
        /// Official documentation from HP webOS Services Overview
        /// Used to designate system services in HP webOS applications		
        /// </remarks>
        public static IResourceScheme palm = new ResourceScheme("palm", "Palm WebOS System Service Protocol", false);	
        
        /// <summary>
        /// Paparazzi Protocol
        /// </summary>
        /// <example>
        /// paparazzi:[<options>]http:[//<host>[:[<port>][<transport>]]/
        /// </example>
        /// <remarks>
        /// Official documentation from the Paparazzi! website
        /// Used to launch and automatically take a screen shot using the application "Paparazzi!"	
        /// </remarks>
        public static IResourceScheme paparazzi = new ResourceScheme("paparazzi", "Paparazzi Protocol", false);

        /// <summary>
        /// Psyc Protocol
        /// </summary>
        /// <example>
        /// psyc:[//<host>[:[<port>][<transport>]]/[<object-name>][#<channel-name>]
        /// </example>
        /// <remarks>
        /// Used to identify or locate a person, group, place or a service and specify its ability to communicate
        /// </remarks>
        public static IResourceScheme psyc = new ResourceScheme("psyc", "Psyc Protocol", false);
        
        /// <summary>
        /// Java Remote Method Invocation Protocol
        /// </summary>
        /// <example>
        /// rmi://<host>[:<port>]/<object-name>
        /// </example>
        /// <remarks>
        /// Sun
        /// Look up a Java object in an RMI registry.			
        /// URI scheme understood by JNDI. Can be used to lookup a remote Java object within an RMI registry (typically for the purposes of RMI on that object).
        /// Host/port in the URI are of the rmiregistry process, not the remote object.
        /// </remarks>
        public static IResourceScheme rmi = new ResourceScheme("rmi", "RMI Registry Protocol", false);

        /// <summary>
        /// Real Time Messaging Protocol
        /// </summary>
        /// <example>
        /// rtmp://<host>/<application>/<media>
        /// </example>
        /// <remarks>
        /// Adobe Systems
        /// URI schema used to connect to Adobe Flash Media Server.
        /// </remarks>
        public static IResourceScheme rtmp = new ResourceScheme("rtmp", "Real Time Messaging Protocol", false);

        /// <summary>
        /// Second Life Protocol
        /// </summary>
        /// <example>
        /// secondlife://<region name>/<x position>/<y position>/<z position>
        /// </example>
        /// <remarks>
        /// Linden Lab
        /// Open the Map floater in Second Life application to teleport the resident to the location.
        /// Used by SLurl.com. Knowledge base article.
        /// </remarks>
        public static IResourceScheme secondlife = new ResourceScheme("secondlife", "Second Life Protocol", false);
        
        /// <summary>
        /// Social Graph Node Mapper Protocol
        /// </summary>
        /// <example>
        /// sgn://social-network.example.com/?ident=bob
        /// </example>
        /// <remarks>
        /// Google
        /// Official documentation from sgnodemapper project.
        /// </remarks>
        public static IResourceScheme sgn = new ResourceScheme("sgn", "Social Graph Node Mapper Protocol", false);
        
        
        /// <summary>
        /// Skype Protocol
        /// </summary>
        /// <example>
        /// skype:<username|phonenumber>[?[add|call|chat|sendfile|userinfo]]
        /// </example>
        /// <remarks>
        /// Official documentation from Skype website.
        /// Launching Skype call (unofficial; see also callto:)	Skype		
        /// </remarks>
        public static IResourceScheme skype = new ResourceScheme("skype", "Skype Protocol", false);

        /// <summary>
        /// Spotify Protocol
        /// </summary>
        /// <example>
        /// spotify:<artist|album|track>:<id>
        /// spotify:search:<text>
        /// spotify:user:<username>:playlist:<id>
        /// spotify:track:2jCnn1QPQ3E8ExtLe6INsx
        /// </example>
        /// <remarks>
        /// Informally specified in Spotify official blog post by CTO Andreas Ehn.
        /// Load a track, album, artist, search, or playlist in Spotify	Spotify	
        /// </remarks>
        public static IResourceScheme spotify = new ResourceScheme("spotify", "Spotify Protocol", false);

        /// <summary>
        /// Secure Shell Protocol
        /// </summary>
        /// <example>
        /// ssh://[<user>[;fingerprint=<host-key fingerprint>]@]<host>[:<port>]	
        /// </example>
        /// <remarks>
        /// IETF Draft
        /// SSH connections (like telnet:)
        /// </remarks>
        public static IResourceScheme ssh = new ResourceScheme("ssh", "Secure Shell Protocol", false);

        /// <summary>
        /// Secure Shell File Transfer Protocol
        /// </summary>
        /// <example>
        /// sftp://[<user>[;fingerprint=<host-key fingerprint>]@]<host>[:<port>]/<path>/<file>
        /// </example>
        /// <remarks>
        /// IETF Draft
        /// SFTP file transfers (not be to confused with FTPS (FTP/SSL))		
        /// </remarks>
        public static IResourceScheme sftp = new ResourceScheme("sftp", "Secure Shell File Transfer Protocol", false);

        /// <summary>
        /// Server Message Block Protocol
        /// </summary>
        /// <example>
        /// smb://[<user>@]<host>[:<port>][/[<path>]][?<param1>=<value1>[;<param2>=<value2>]]
        /// smb://[<user>@]<workgroup>[:<port>][/]
        /// </example>
        /// <remarks>
        /// IETF Draft
        /// Accessing SMB/CIFS shares	
        /// </remarks>
        public static IResourceScheme smb = new ResourceScheme("smb", "Server Message Block Protocol", false);	
        
        /// <summary>
        /// Soldat Protocol
        /// </summary>
        /// <example>
        /// soldat://<host>:<port>/[password]
        /// soldat://127.0.0.1:23073/thatssecret!
        /// </example>
        /// <remarks>
        /// Official note in Manual
        /// </remarks>
        public static IResourceScheme soldat = new ResourceScheme("soldat", "Soldat Protocol", false);	
 
        /// <summary>
        /// Steam Protocol
        /// </summary>
        /// <example>
        /// steam:<command line arguments>
        /// steam://<action>/<id, addon, IP, hostname, etc.>
        /// </example>
        /// <remarks>
        /// Valve Corporation
        /// Official documentation from Valve Developer Community website
        /// Interact with Steam: install apps, purchase games, run games, etc.
        /// </remarks>
        public static IResourceScheme steam = new ResourceScheme("steam", "Steam Protocol", false);

        /// <summary>
        /// Subversion Protocol
        /// </summary>
        /// <example>
        /// svn[+ssh]://<logindetails>@<repository><:port>/<modulepath>
        /// </example>
        /// <remarks>
        /// Provides a link to a Subversion (SVN) source control repository	Subversion
        /// </remarks>
        public static IResourceScheme svn = new ResourceScheme("svn", "Subversion Protocol", false);
	
        /// <summary>
        /// TeamSpeak Protocol
        /// </summary>
        /// <example>
        /// teamspeak://<server>[:<port>]/[?<parameter1>=<value1>[&<parameter2>=<value2>]]
        /// </example>
        /// <remarks>
        /// Official documentation from TeamSpeak Website
        /// Joining a server
        /// </remarks>
        public static IResourceScheme teamspeak = new ResourceScheme("teamspeak", "TeamSpeak Protocol", false);

        /// <summary>
        /// Things Protocol
        /// </summary>
        /// <example>
        /// things:add?title=Buy%20milk&notes=Low%20fat&dueDate=2011-12-24
        /// </example>
        /// <remarks>
        /// Cultured Code
        /// Send a to-do to Things.
        /// This URI scheme works on Mac OS & iOS, providing Things by Cultured Code is installed.
        /// </remarks>
        public static IResourceScheme things = new ResourceScheme("things", "Things Protocol", false);


        /// <summary>
        /// Unreal Legacy Protocol
        /// </summary>
        /// <example>
        /// unreal://<server>[:<port>]/
        /// </example>
        /// <remarks>
        /// Unreal
        /// Joining servers	
        /// </remarks>
        public static IResourceScheme unreal = new ResourceScheme("unreal", "Unreal Legacy Protocol", false);

        /// <summary>
        /// Unreal Tournament 2004 Protocol
        /// </summary>
        /// <example>
        /// ut2004://<server>[:<port>][/<map>?<options>]
        /// </example>
        /// <remarks>
        /// Documentation from Unreal Developer Network
        /// Joining servers	Unreal Tournament 2004	
        /// </remarks>
        public static IResourceScheme ut2004 = new ResourceScheme("ut2004", "Unreal Tournament 2004 Protocol", false);

        /// <summary>
        /// Ventrilio Protocol
        /// </summary>
        /// <example>
        /// ventrilo://<server>[:<port>]/[?<parameter1>=<value1>[&<parameter2>=<value2>]]
        /// </example>
        /// <remarks>
        /// Ventrilo
        /// Official documentation from Ventrilo Website
        /// Joining a server.	
        /// </remarks>
        public static IResourceScheme ventrilio = new ResourceScheme("ventrilo", "Ventrilio Protocol", false);

        /// <summary>
        /// View Source Protocol
        /// </summary>
        /// <example>
        /// view-source:<absolute-URI> where <absolute-URI> is specified in RFC 3986.
        /// view-source:http://en.wikipedia.org/wiki/URI_scheme
        /// </example>
        /// <remarks>	
        /// See view-source URI scheme for details.
        /// Shows a web page as code 'in the raw'.
        /// </remarks>
        public static IResourceScheme view_source = new ResourceScheme("view-source", "View Source Protocol", false);

        /// <summary>
        /// iCalendar Protocol
        /// </summary>
        /// <example>
        /// webcal://<hierarchical part>
        /// webcal://example.com/calendar.ics
        /// </example>
        /// <remarks>
        /// HTTP as a transport protocol is assumed.
        /// See Webcal for details.
        /// Subscribing to calendars in iCalendar format
        /// </remarks>
        public static IResourceScheme webcal = new ResourceScheme("webcal", "ICalendar Protocol", false);

        /// <summary>
        /// WebSocket Protocol
        /// </summary>
        /// <example>
        /// ws:<hierarchical part>
        /// </example>
        /// <remarks>
        /// IETF Draft
        /// </remarks>
        public static IResourceScheme ws = new ResourceScheme("ws", "WebSocket Protocol", false);

        /// <summary>
        /// Secure WebSocket Protocol
        /// </summary>
        /// <example>
        /// wss:<hierarchical part>
        /// </example>
        /// <remarks>
        /// IETF Draft
        /// </remarks>
        public static IResourceScheme wss = new ResourceScheme("wss", "Secure WebSocket Protocol", false);

	    /// <summary>
	    /// Wireless Telephony Application Interface Protocol
	    /// </summary>
        /// <example>
        /// wtai://wp/mc/+18165551212
        /// </example>
        /// <remarks>
        /// WAP Forum
        /// See Application Protocol Wireless Application Environment Specification Version 1.1 for details.
        /// </remarks>
        public static IResourceScheme wtai = new ResourceScheme("wtai", "Wireless Telephony Application Interface Protocol", false);

        /// <summary>
        /// What You Can Cache Is What You Get Protocol
        /// </summary>
        /// <example>
        /// wyciwyg://<URI>
        /// </example>
        /// <remarks>
        /// Mozilla
        /// See WYCIWYG for details.
        /// </remarks>
        public static IResourceScheme wyciwyg = new ResourceScheme("wyciwyg", "What You Can Cache Is What You Get Protocol", false);

        /// <summary>
        /// Xfire Protocol
        /// </summary>
        /// <example>
        /// xfire:<function>[?<parameter1>=<value1>[&<parameter2>=<value2>]]
        /// </example>
        /// <remarks>
        /// Xfire
        /// Official documentation from Xfire website
        /// Adding friends and servers, joining servers, changing status text.	
        /// </remarks>
        public static IResourceScheme xfire = new ResourceScheme("xfire", "Xfire Protocol", false);

        /// <summary>
        /// Extensible Resource Identifier Protocol
        /// </summary>
        /// <example>
        /// xri://<authority>[/[<path>]][?<query>][#fragment]
        /// </example>
        /// <remarks>
        /// OASIS XRI Technical Committee
        /// Official documentation from OASIS XRI Technical Committee
        /// </remarks>
        public static IResourceScheme xri = new ResourceScheme("xri", "Extensible Resource Identifier Protocol", false);

        /// <summary>
        /// Yahoo Instant Messenger Protocol
        /// </summary>
        /// <example>
        /// ymsgr:sendIM?<screenname>
        /// </example>
        /// <remarks>
        /// Yahoo! Messenger
        /// Sending an instant message to a Yahoo! Contact.	
        /// </remarks>
        public static IResourceScheme ymsgr = new ResourceScheme("ymsgr", "Yahoo Instant Messenger Protocol", false);

        #endregion
    }
}
