using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrakYazilimLib.Util.helper
{
    public class Fistf
    {

        private string str;
        //private final Map<String, Object> arguments = new HashMap();
        //private List<Object> posArguments = new ArrayList();

        private Fistf(string str)
        {
            this.str = str;
        }
        
        /// <summary>
        /// sorgu içerisinde @_{parametre_ismi}_ kalıbına uyan yere count sayısı kadar parametre ekler. 
        /// örnegin st ve 2 verirsek @_st_ alanını @st1,@st2 şekline çevirir
        /// </summary>        
        public Fistf multiParam(string prmName, int count)
        {
            Regex r = new Regex($"@_{prmName}_");
            str= r.Replace(str, sqlPrmCevir(prmName, count));
            return this;
        }

        public static Fistf stf(string str)
        {
            return new Fistf(str);
        }

        public string getValue() => str;

        public string sqlPrmCevir(string prmName, int size)
        {
            string fullprm = "";
            for (int index = 0; index < size; index++)
            {
                if (index > 0) fullprm += ",";
                fullprm += "@" + prmName + index;
            }
            return fullprm;
        }

        //public static OzFormatter stf(String str, Object...args)
        //{
        //    OzFormatter af = new OzFormatter(str);
        //    //if (args != null)
        //    //{
        //    //    af.posArguments = Arrays.asList(args);
        //    //}

        //    return af;
        //}

        //  public static void main(String[] args)
        //  {

        //      //str();
        //  }

        //  public String jqlfmt()
        //  {
        //      //StringBuilder result = new StringBuilder();
        //      //StringBuilder param = new StringBuilder(16);
        //      //AlephFormatter.State state = AlephFormatter.State.FREE_TEXT;
        //      String strformatted = str;

        //      if (arguments.size() > 0)
        //      {
        //          for (Map.Entry<String, Object> entry : arguments.entrySet())
        //          {
        //              //System.out.println(entry.getKey() + "/" + entry.getValue());
        //              //String key = entry.getKey();
        //              strformatted = strformatted.replaceAll(":" + entry.getKey(), entry.getValue().toString());
        //          }
        //      }

        //      return strformatted;
        //  }

        //  /**
        //* @return
        //* @p0, @p1 değişkenleri posArguments listesindeki elemanlar ile değiştiriyor. Başına : koyar. :element
        //*/
        //  public String sqlfmtPnumber()
        //  {
        //      //StringBuilder result = new StringBuilder();
        //      //StringBuilder param = new StringBuilder(16);
        //      //AlephFormatter.State state = AlephFormatter.State.FREE_TEXT;
        //      String strformatted = str;

        //      if (posArguments.size() > 0)
        //      {
        //          for (Integer index = 0; index < posArguments.size(); index++)
        //          {
        //              //System.out.println(entry.getKey() + "/" + entry.getValue());
        //              //String key = entry.getKey();
        //              strformatted = strformatted.replaceAll("@p" + index.toString(), ":" + posArguments.get(index).toString());
        //          }
        //      }

        //      return strformatted;
        //  }

        //  /**
        //* @_ ifadelerini : ye çevirir.
        //*
        //* @return
        //*
        //*/
        //  public String sqlFmtConvertAtDash()
        //  {
        //      //StringBuilder result = new StringBuilder();
        //      //StringBuilder param = new StringBuilder(16);
        //      //AlephFormatter.State state = AlephFormatter.State.FREE_TEXT;
        //      String strformatted = str;

        //      //System.out.println(entry.getKey() + "/" + entry.getValue());
        //      //String key = entry.getKey();
        //      strformatted = strformatted.replaceAll("@_", ":");

        //      return strformatted;
        //  }

        //  /**
        //*
        //* @ ifadelerini : ye çevirir.
        //*
        //* @return
        //*
        //*/
        //  public String sqlFmtAt()
        //  {
        //      //StringBuilder result = new StringBuilder();
        //      //StringBuilder param = new StringBuilder(16);
        //      //AlephFormatter.State state = AlephFormatter.State.FREE_TEXT;
        //      String strformatted = str;

        //      //System.out.println(entry.getKey() + "/" + entry.getValue());
        //      //String key = entry.getKey();
        //      strformatted = strformatted.replaceAll("@", ":");

        //      return strformatted;
        //  }

        //  public String sqlListAt()
        //  {

        //      //StringBuilder result = new StringBuilder();
        //      //StringBuilder param = new StringBuilder(16);
        //      //AlephFormatter.State state = AlephFormatter.State.FREE_TEXT;
        //      String strformatted = str;

        //      String regex = "(?s)\\(@(.*?)\\)";  //?s includes newline

        //      //		Pattern.compile("(?<myGroup>[A-Za-z])[0-9]\\k<myGroup>")
        //      //				.matcher("a9a c0c d68")
        //      //				.find();//matches:  'a9a' at 0-3, 'c0c' at 4-7
        //      //		//'a9a c0c d68'

        //      Pattern pattern = Pattern.compile(regex);
        //      Matcher matcher = pattern.matcher(strformatted);

        //      //System.out.println("Text:"+strformatted);
        //      //System.out.println("Group Count:"+matcher.groupCount());

        //      //if(!matcher.matches()) return strformatted;

        //      //	while (matcher.find()){
        //      //  System.out.println(matcher.group());
        //      //
        //      //		}

        //      String str2 = matcher.replaceAll("(<$1>)");

        //      str2 = str2.replaceAll("@", ":");
        //      //System.out.println("Text:"+str2);

        //      //		for (int i = 1; i < matcher.groupCount() ; i++) {
        //      //
        //      //			System.out.println(" Grup"+i+" :"+matcher.group(i));
        //      //
        //      //
        //      //		}



        //      //System.out.println(entry.getKey() + "/" + entry.getValue());
        //      //String key = entry.getKey();
        //      //strformatted = strformatted.replaceAll("@", ":");

        //      return str2;
        //  }

        //  /**
        //*
        //* {0} {1} {2} sayısal yer imlerini , (pos) argünmanlar ile yer değiştirir.
        //* Dikkat 0 dan başlanmalı
        //*
        //* @return
        //*/
        //  public String fmt()
        //  {

        //      if (posArguments.size() > 0)
        //      {
        //          return numfmt();
        //      }

        //      return str;
        //  }

        //  public String numfmt()
        //  {

        //      String strformmatted = str;

        //      if (posArguments.size() > 0)
        //      {
        //          for (Integer index = 0; index < posArguments.size(); index++)
        //          {
        //              //System.out.println(entry.getKey() + "/" + entry.getValue());
        //              //String key = entry.getKey();
        //              strformmatted = strformmatted.replaceAll("\\{" + index.toString() + "\\}", posArguments.get(index).toString());
        //          }
        //      }

        //      return strformmatted;
        //  }

        //  public OzFormatter arg(String argName, Object object)
        //  {
        //      this.failIfArgExists(argName);
        //      this.arguments.put(argName, object);
        //      return this;
        //  }

        //  public void failIfArgExists(String argName)
        //  {
        //      if (this.arguments.containsKey(argName))
        //      {
        //          throw UncheckedFormatterException.argumentAlreadyExist(argName);
        //      }
        //  }

    }
}
