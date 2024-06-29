using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrakYazilimLib.DbUtil
{
    public class FiMetaEntity
    {
        private FiMetaField genFiMetaField(PropertyInfo propField)
        {
            FiMetaField fiMetaField = new FiMetaField();

            FiColumnAttribute fiColumnAttribute = null;
            //FiIdAttribute fiIdAttribute = null;
            
            fiMetaField.field = propField.Name;

            // ***********  Attribute değişkenleri set edilir
            foreach (var loopAttribute in propField.GetCustomAttributes())
            {
                //Console.WriteLine("Attribute Name : {0}", attribute.GetType().Name);

                if (loopAttribute.GetType() == typeof(FiTransientAttribute))
                {
                    fiMetaField.isTransient = true;
                    fiMetaField.isIgnore = true;
                    //return fiMetaField;
                }

                if (loopAttribute.GetType() == typeof(FiColumnAttribute))
                {
                    fiColumnAttribute = loopAttribute as FiColumnAttribute;
                }


                if (loopAttribute.GetType() == typeof(FiIdAttribute))
                {
                    //fiIdAttribute = loopAttribute as FiIdAttribute;
                    fiMetaField.isIDField = true;
                }
            }
            // ----------------------------------------- End 

            if (fiColumnAttribute != null &&  fiColumnAttribute.defaultValue!=null  && !fiColumnAttribute.defaultValue.Equals(""))
            {
                fiMetaField.existDefault = true;
            }

            

            return fiMetaField;
            //return "string";
        }

        public List<FiMetaField> fieldsList(Type typeClazz)
        {
            var propsEntity = typeClazz.GetProperties();  //typeStudent.GetProperties();

            var listFields = new List<FiMetaField>();

            foreach (var field in propsEntity)
            {
                //Console.WriteLine($"Prop Name :{field.Name} Type {field.PropertyType}");

                FiMetaField fiMetaField = genFiMetaField(field);
                listFields.Add(fiMetaField);
            }

            return listFields;
        }

    }
}
