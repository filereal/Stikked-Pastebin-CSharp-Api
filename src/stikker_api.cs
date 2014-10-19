using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.IO;
namespace Stikked_API
{
    class stikked
    {
        /*
         * Stikked API 
         */
        string globaldomain;
        public string Domain
        {
            get
            {
                return globaldomain;
            }
            set
            {
                if (value.EndsWith("/")){
                    value = value.Substring(0,value.Length - 1);
                }
                globaldomain = value;
            }
        }
        public string Get_Json(string apiname){
            WebClient wc = new WebClient();
            return wc.DownloadString(Domain + "/api/" + apiname);
        }
        public dynamic get_recent()
        {
            string json = Get_Json("recent");
            dynamic jArr2 = JsonConvert.DeserializeObject(json);
            return jArr2;
        }
        string curprocess = "";
        public string currentprocessing
        {
            get
            {
                return curprocess;
            }
            set
            {
                curprocess = value;
            }
        }
        public dynamic get_paste(string pid)
        {

            string json = Get_Json("paste/"+ pid);
            dynamic j = JObject.Parse(json);
            return j;
           
        }
        public string CreatePaste(string text,string name,int isPrivate,string lang,string expiry)
        {
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(Domain + "/api/create");
            
            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "text=" + text;
            postData += "&name=" + name;
            postData += "&private=" + isPrivate;
            postData += "&lang=" + lang;
            postData += "&expire=" + expiry;
            byte[] data = encoding.GetBytes(postData);

            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/x-www-form-urlencoded";
            httpWReq.ContentLength = data.Length;

            using (Stream stream = httpWReq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            System.IO.StreamReader sr = new StreamReader(response.GetResponseStream());
            return sr.ReadToEnd(); //Return Paste url
        }
        public string[] getlanglist()
        {
            string langs = "text, html5, css, javascript, php, python, ruby, lua, bash, erlang, go, c, cpp, diff, latex, sql, xml, 0, 4cs, 6502acme, 6502kickass, 6502tasm, 68000devpac, abap, actionscript, actionscript3, ada, algol68, apache, applescript, apt_sources, arm, asm, asymptote, asp, autoconf, autohotkey, autoit, avisynth, awk, bascomavr, basic4gl, bf, bibtex, blitzbasic, bnf, boo, c_loadrunner, c_mac, caddcl, cadlisp, cfdg, cfm, chaiscript, cil, clojure, cmake, cobol, coffeescript, csharp, cuesheet, d, dcs, dcl, dcpu16, delphi, div, dos, dot, e, ecmascript, eiffel, email, epc, euphoria, f1, falcon, fo, fortran, freebasic, freeswitch, fsharp, gambas, gdb, genero, genie, gettext, glsl, gml, gnuplot, groovy, gwbasic, haskell, haxe, hicest, hq9plus, html4strict, icon, idl, ini, inno, intercal, io, j, java, java5, jquery, klonec, klonecpp, lb, ldif, lisp, llvm, locobasic, logcat, logtalk, lolcode, lotusformulas, lotusscript, lscript, lsl2, m68k, magiksf, make, mapbasic, matlab, mirc, mmix, modula2, modula3, mpasm, mxml, mysql, nagios, netrexx, newlisp, nsis, oberon2, objc, objeck, ocaml, octave, oobas, oorexx, oracle11, oracle8, oxygene, oz, parasail, parigp, pascal, pcre, per, perl, perl6, pf, pic16, pike, pixelbender, pli, plsql, postgresql, povray, powerbuilder, powershell, proftpd, progress, prolog, properties, providex, purebasic, pys60, q, qbasic, rails, rebol, reg, rexx, robots, rpmspec, rsplus, sas, scala, scheme, scilab, sdlbasic, smalltalk, smarty, spark, sparql, stonescript, systemverilog, tcl, teraterm, thinbasic, tsql, typoscript, unicon, uscript, upc, urbi, vala, vb, vbnet, vedit, verilog, vhdl, vim, visualfoxpro, visualprolog, whitespace, whois, winbatch, xbasic, xorg_conf, xpp, yaml, z80, zxbasic";
            langs = langs.Replace(" ","");
            string[] alllangs = langs.Split(',');
            return alllangs;
        }
    }
}
