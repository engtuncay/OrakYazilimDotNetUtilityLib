using System;
using System.Dynamic;
using System.Web.UI.WebControls;
using OrakYazilimLib.Util;
using OrakYazilimLib.Util.core;
using System.Data;

namespace OrakYazilimLib.DataContainer
{
  public class Fdr<T>
  {
    private bool? _boExecution;

    public bool? boExecution
    {
      get
      {
        return _boExecution;
      }

      set
      {
        _boExecution = value;
        blResult = value;
      }
    }

    public bool? boTknValid  { get; set;}

    /// <summary>
    /// deprecated - boExecution kullan
    /// </summary>
    public bool? blResult { get; set; }

    public bool? boResult { get; set; }
    public T obReturn { get; set; }

    private T _refValue;
    public T refValue
    {
      get
      {
        return _refValue;
      }
      set
      {
        _refValue = value;
        obReturn = value;
      }
    }
    public string txErrorMsgShort { get; set; }
    public string txErrorMsgDetail { get; set; }
    public int lnRowsAffected { get; set; }
    public int? lnIdAffected { get; set; }
    public int? lnTotalLength { get; set; }
    public object spec1 { get; set; }

    public int? lnStatusCode { get; set; }

    public string txId { get; set; }

    public string txMessage { get; set; }

    public Exception refException { get; set; }

    /**
     * External Object
     */
    public void SetExtObject<TS>(TS value)
    {
      this.spec1 = value;
    }

    public TS GetExtObject<TS>()
    {
      return (TS)this.spec1;
    }

    /**
 * İşlem sonuçlarının hepsi true olursa sonuç true olur, bir tane false varsa sonuç false olur.
 *
 * Tüm İşlemlerde Birleştirilen Alanlar : Log, Message, Exception
 *
 * Value ile birleştirme vs yapmaz.
 *
 * @param fdrSubWork Birleştirilecek Fdr (alt fdr işi)
 */
    public  void CombineAnd<TPrmeA>(Fdr<TPrmeA> fdrSubWork)
    {

      // And işlemi olduğu false sonuç, boExecution false yapar
      if (FiBoolean.IsFalse(fdrSubWork.boExecution))
      {
        boExecution = false;
        //setLnFailureOpCount(getLnFailureOpCountInit() + 1);
      }

      if (FiBoolean.IsTrue(fdrSubWork.boExecution))
      {
        //setLnSuccessOpCount(getLnSuccessOpCountInit() + 1);
        boExecution ??= true;
      }

      // null sonuçlara özel combine işlemi
//        if (fdrSubWork.getBoResult() == null) {
//
//        }
      // if(FiBool.isTrue(getBoMultiFdr())){
      //   getFdrListInit().add(fdrSubWork);
      // }

      // Tümü için yapılacaklar
      if (fdrSubWork.refException != null)
      {
        refException ??= fdrSubWork.refException;
        // exception birden fazla olma ihtimali var.
        //getListExceptionInit().add(fdrSubWork.getException());
      }

      // Tüm işlemlerde mesaj birleştirilir.
      if (!FiString.IsEmpty(fdrSubWork.txMessage)) AppendMessageLn(fdrSubWork.txMessage);

      // Loglar birleştirilir.
      //if (!FiCollection.isEmpty(fdrSubWork.getLogList())) getLogListInit().addAll(fdrSubWork.getLogList());

      // appendRowsAffected(fdrSubWork.getRowsAffectedOrEmpty());
      // appendLnUpdated(fdrSubWork.getLnUpdatedRows());
      // appendLnInserted(fdrSubWork.getLnInsertedRows());
      // appendLnDeleted(fdrSubWork.getLnDeletedRows());

      // Birleştirme yapıldığı için eski Fdr'ye log eklenmesi engellenir
      // fdrSubWork.setBoLockAddLog(true);
    }
    public void AppendMessageLn(string txValue)
    {
      txMessage = txMessage + (!FiString.IsEmpty(txMessage)?"\n":"") + txValue;
    }

    public void AppendMessageWithSc(string txValue)
    {
      txMessage = txMessage + (!FiString.IsEmpty(txMessage)?";;":"") + txValue;
    }

    public Fdr(bool prmBlResult) { this.blResult = prmBlResult; }

    public Fdr(int prmLnRowsAffected)
    {
      this.lnRowsAffected = prmLnRowsAffected;

      if (prmLnRowsAffected > 0)
      {
        this.blResult = true;
      }
      else
      {
        this.blResult = false;
      }

    }

    public Fdr() {}

    public static Fdr<T> FactoryScopeId(int prmIdAffected)
    {
      var fiReturn = new Fdr<T>();
      fiReturn.lnIdAffected = prmIdAffected;
      fiReturn.lnRowsAffected = prmIdAffected;

      if (prmIdAffected > 0)
      {
        fiReturn.blResult = true;
      }
      else
      {
        fiReturn.blResult = false;
      }

      return fiReturn;
    }

    public static Fdr<T> FactoryObject(T returnObject)
    {
      var fiReturn = new Fdr<T>();
      fiReturn.obReturn = returnObject;
      return fiReturn;
    }

    public void ExceptionQueryErrorLog(Exception exception)
    {
      this.txErrorMsgShort = exception.Message;
      this.txErrorMsgDetail = FiLogWeb.getStackTrace(exception);
      this.lnRowsAffected = -1;
      this.blResult = false;
      this.boExecution = false;
    }

    public bool isTrueResult()
    {
      if (this.blResult == null) return false;
      return blResult.Value;
    }

    public bool IsTrueBoResult()
    {
      if (this.blResult == null) return false;
      return blResult.Value;
    }

    public Fdr<T> buiMess(string txMessage)
    {
      this.txMessage = txMessage;
      return this;
    }
  }

  public class Fdr : Fdr<object>
  {
    public Fdr()
    {

    }

    public Fdr(bool v)
    {
      base.boExecution = v;
    }

  }
}