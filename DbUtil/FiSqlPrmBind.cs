using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using OrakYazilimLib.DbGeneric;
using OrakYazilimLib.Util.core;

namespace OrakYazilimLib.DbUtil
{
    public class FiSqlPrmBind<T>
    {
        public List<FiSqlParameter> BindObjectAll(T entity)
        {
            var typeClazz = typeof(T);

            List<FiSqlParameter> listPrm = new List<FiSqlParameter>();

            var propsEntity = typeClazz.GetProperties();

            var listFields = new FiMetaEntity().fieldsList(typeClazz);  //FieldsList(typeClazz);

            foreach (var fieldDefinition in listFields)
            {
                if (fieldDefinition.isIDField || fieldDefinition.isTransient) continue;

                FiSqlParameter fiPrm = new FiSqlParameter();
                fiPrm.field = "@" + fieldDefinition.field;
                fiPrm.clField = fieldDefinition.field;
                object fieldObj = FiReflection.GetPropValue(entity, fieldDefinition.field);

                if (fieldObj == null && fieldDefinition.existDefault)
                {
                    fiPrm.isDefault = true;
                }

                fiPrm.value = fieldObj;

                listPrm.Add(fiPrm);
            }

            return listPrm;
        }

        

        
    }
}