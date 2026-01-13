using System;
using OrakYazilimLib.Util;
using OrakYazilimLib.Util.Collection;
using OrakYazilimLib.Util.core;
using OrakYazilimLib.Util.FiEntity;
using OrakYazilimLib.Util.FiMetas;
using System.Collections.Generic;
using System.Data;

namespace OrakYazilimLib.DataContainer
{
  public class Fdr<T> : IFdr
  {
    private bool? _boResult;

    public bool? boResult
    {
      get
      {
        return _boResult;
      }
      set
      {
        _boResult = value;
        blResult = value;
      }
    }

    /**
     * boExecution false olursa, sorgunun server vs bağlantı kurulamadığını gösterir.
     *
     * boResult,boExecution yapıldığı için bazı yerlerde mantık hatası var
     *
     * Kullanılmamaya çalışılmalı
     *
     */
    public bool? boExecution { get; set; }

    public bool? boTknValid { get; set; }

    /**
     * Deprecated - boResult kullanılmalı
     */
    [Obsolete("boResult kullanılmalı")]
    public bool? blResult
    {
      get
      {
        return _boResult;
      }
      set
      {
        _boResult = value;
      }
    }


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
    public int? fqpLnTotal { get; set; }
    public object spec1 { get; set; }

    public int? lnStatusCode { get; set; }

    public string txId { get; set; }

    public string txMessage { get; set; }

    public Exception refException { get; set; }

    public string txResponse { get; set; }

    //public string txResponse2 { get; set; }

    /**
     * İşlem Dönüşü Fkb Değeri
     */
    public FiKeybean fkbVal { get; set; }

    /**
     * İşlem Dönüşü FkbList değeri
     */
    public FkbList refFkbListVal { get; set; }

    /**
     * İşlem Dönüşü DataTable değeri
     */
    public DataTable refDtbVal { get; set; }

    public List<FieLog> listFieLog { get; set; }

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

    // XIMSNIP ifnull yapısı
    public List<FieLog> GetListFieLogInit()
    {
      return listFieLog ??= new List<FieLog>();
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
    public void CombineAndByBoExec<TPrmeA>(Fdr<TPrmeA> fdrSubWork)
    {

      // And işlemi olduğu false sonuç, boExecution false yapar
      if (FiBool.IsFalse(fdrSubWork.boExecution))
      {
        boExecution = false;
        //setLnFailureOpCount(getLnFailureOpCountInit() + 1);
      }

      if (FiBool.IsTrue(fdrSubWork.boExecution))
      {
        //setLnSuccessOpCount(getLnSuccessOpCountInit() + 1);
        boExecution ??= true;
      }

      // null sonuçlara özel combine işlemi
      //  if (fdrSubWork.getBoResult() == null) {
      //
      // }

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

    /**
     * fdrSub boResult null ise, boResult değiştirilmez
     */
    public void CombineAnd<PrmT>(Fdr<PrmT> fdrSub)
    {
      if (fdrSub == null) return;

      // And işlemi olduğu false sonuç, boExecution false yapar
      if (FiBool.IsFalse(fdrSub.boResult))
      {
        boResult = false;
        //setLnFailureOpCount(getLnFailureOpCountInit() + 1);
      }

      if (FiBool.IsTrue(fdrSub.boResult))
      {
        //setLnSuccessOpCount(getLnSuccessOpCountInit() + 1);
        boResult ??= true;
      }


      // null olduğunda boResult sonucunu değiştirme
//        if (fdrSubWork.getBoResult() == null) {
//
//        }

      // if(FiBool.isTrue(getBoMultiFdr())){
      //   getFdrListInit().add(fdrSubWork);
      // }

      // Tüm işlemlerde mesaj birleştirilir.
      // Loglar birleştirilir.
      CombineLogsAndMess(fdrSub);

      // Tümü için yapılacaklar
      if (fdrSub.refException != null)
      {
        refException ??= fdrSub.refException;
        // exception birden fazla olma ihtimali var.
        //getListExceptionInit().add(fdrSubWork.getException());
      }

      // appendRowsAffected(fdrSubWork.getRowsAffectedOrEmpty());
      // appendLnUpdated(fdrSubWork.getLnUpdatedRows());
      // appendLnInserted(fdrSubWork.getLnInsertedRows());
      // appendLnDeleted(fdrSubWork.getLnDeletedRows());

      // Birleştirme yapıldığı için eski Fdr'ye log eklenmesi engellenir
      // fdrSubWork.setBoLockAddLog(true);
    }

    /**
     * fdrSub boResult değerinin tersini alarak birleştirir
     */
    public void CombineAndReverse<PrmT>(Fdr<PrmT> fdrSub)
    {
      if (fdrSub == null) return;

      if (fdrSub.boResult != null)
      {
        // And işlemi olduğu false sonuç, boExecution false yapar
        if (FiBool.IsFalse(!fdrSub.boResult))
        {
          boResult = false;
          //setLnFailureOpCount(getLnFailureOpCountInit() + 1);
        }

        if (FiBool.IsTrue(!fdrSub.boResult))
        {
          //setLnSuccessOpCount(getLnSuccessOpCountInit() + 1);
          boResult ??= true;
        }
      }

      // null olduğunda boResult sonucunu değiştirme
      //if (fdrSubWork.getBoResult() == null) {
      //
      // }

      // if(FiBool.isTrue(getBoMultiFdr())){
      //   getFdrListInit().add(fdrSubWork);
      // }

      // Tüm işlemlerde mesaj birleştirilir.
      // Loglar birleştirilir.
      CombineLogsAndMess(fdrSub);

      // Tümü için yapılacaklar
      if (fdrSub.refException != null)
      {
        refException ??= fdrSub.refException;
        // exception birden fazla olma ihtimali var.
        //getListExceptionInit().add(fdrSubWork.getException());
      }

      // appendRowsAffected(fdrSubWork.getRowsAffectedOrEmpty());
      // appendLnUpdated(fdrSubWork.getLnUpdatedRows());
      // appendLnInserted(fdrSubWork.getLnInsertedRows());
      // appendLnDeleted(fdrSubWork.getLnDeletedRows());

      // Birleştirme yapıldığı için eski Fdr'ye log eklenmesi engellenir
      // fdrSubWork.setBoLockAddLog(true);
    }

    public void CombineAnd2(IFdr fdrSub)
    {

      // And işlemi olduğu false sonuç, boExecution false yapar
      if (FiBool.IsFalse(fdrSub.boResult))
      {
        boResult = false;
        //setLnFailureOpCount(getLnFailureOpCountInit() + 1);
      }

      if (FiBool.IsTrue(fdrSub.boResult))
      {
        //setLnSuccessOpCount(getLnSuccessOpCountInit() + 1);
        boResult ??= true;
      }

      // null olduğunda boResult sonucunu değiştirme
//        if (fdrSubWork.getBoResult() == null) {
//
//        }

      // if(FiBool.isTrue(getBoMultiFdr())){
      //   getFdrListInit().add(fdrSubWork);
      // }

      // Tüm işlemlerde mesaj birleştirilir.
      // Loglar birleştirilir.
      CombineLogsAndMess(fdrSub);

      // Tümü için yapılacaklar
      if (fdrSub.refException != null)
      {
        refException ??= fdrSub.refException;
        // exception birden fazla olma ihtimali var.
        //getListExceptionInit().add(fdrSubWork.getException());
      }

      // appendRowsAffected(fdrSubWork.getRowsAffectedOrEmpty());
      // appendLnUpdated(fdrSubWork.getLnUpdatedRows());
      // appendLnInserted(fdrSubWork.getLnInsertedRows());
      // appendLnDeleted(fdrSubWork.getLnDeletedRows());

      // Birleştirme yapıldığı için eski Fdr'ye log eklenmesi engellenir
      // fdrSubWork.setBoLockAddLog(true);
    }

    public void AppendMessageLn(string txValue)
    {
      txMessage = txMessage + (!FiString.IsEmpty(txMessage) ? "\n" : "") + txValue;
    }

    public void AppendMessageWithSc(string txValue)
    {
      txMessage = txMessage + (!FiString.IsEmpty(txMessage) ? ";;" : "") + txValue;
    }

    public Fdr(bool boResult) { this.boResult = boResult; }

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

    public static Fdr<T> BuiObject(T returnObject)
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

    public bool IsTrueBlResult()
    {
      if (this.blResult == null) return false;
      return blResult.Value;
    }

    public bool IsTrueBoResult()
    {
      if (this.boResult == null) return false;
      return boResult.Value;
    }

    public bool IsTrueBoExec()
    {
      if (this.boExecution == null) return false;
      return boExecution.Value;
      ;
    }

    public Fdr<T> BuiMess(string txMessage)
    {
      this.txMessage = txMessage;
      return this;
    }

    public void CombineLogsAndMess<PrmT>(Fdr<PrmT> fdrSubWork)
    {
      // Tüm işlemlerde mesaj birleştirilir.
      if (!FiString.IsEmpty(fdrSubWork.txMessage)) AppendMessageLn(fdrSubWork.txMessage);

      // Loglar Birleştirilir
      GetListFieLogInit().AddRange(fdrSubWork.GetListFieLogInit());

    }

    public void CombineLogsAndMess(IFdr fdrSubWork)
    {
      // Tüm işlemlerde mesaj birleştirilir.
      if (!FiString.IsEmpty(fdrSubWork.txMessage)) AppendMessageLn(fdrSubWork.txMessage);

      // Loglar Birleştirilir
      GetListFieLogInit().AddRange(fdrSubWork.GetListFieLogInit());
    }

    public void AddLogInfo(string txMess)
    {
      GetListFieLogInit().Add(new FieLog(FimLogTypes.Info(), txMess));
      ;
    }

    public void AddLogError(string txMess)
    {
      GetListFieLogInit().Add(new FieLog(FimLogTypes.Error(), txMess));
    }

    public void AddLogAlert(string txMess)
    {
      GetListFieLogInit().Add(new FieLog(FimLogTypes.Alert(), txMess));
    }

    public void AddLogNotice(string txMess)
    {
      GetListFieLogInit().Add(new FieLog(FimLogTypes.Notice(), txMess));
    }

    public void SetBoExecAndResultFalse()
    {
      this.boExecution = false;
      this.boResult = false;
    }

    public void SetBoExecAndResultTrue()
    {
      this.boExecution = true;
      this.boResult = true;
    }


  }

  public class Fdr : Fdr<object>
  {
    public Fdr()
    {

    }

    public Fdr(bool boResult)
    {
      base.boResult = boResult;
    }






    public static Fdr BuiBoResult(bool? boResult)
    {
      Fdr fdr = new Fdr
      {
        boResult = boResult
      };

      return fdr;
    }
  }
}