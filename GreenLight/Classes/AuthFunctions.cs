using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace GreenLight.Auth
{
    struct AccessLevel
    {
        public bool read;
        public bool write;
    }

    class Rights
    {
        public AccessLevel offer_selector;
        public AccessLevel questionary;
        public AccessLevel table_struct;
        public AccessLevel reference_struct;
        public AccessLevel clause_editor;
        public AccessLevel questionary_editor;
        public AccessLevel active_session;
        public AccessLevel string_replace;
        public AccessLevel table_credprogr;
        public AccessLevel table_clients;
        public AccessLevel data_copy;
        public AccessLevel references;
        public AccessLevel access_control;
        public AccessLevel clause_test;

        public Rights()
        {
            Type r_type = typeof(Rights);

            FieldInfo[] fi_array = r_type.GetFields();

            foreach (FieldInfo fi in fi_array)
            {
                r_type.InvokeMember(fi.Name, BindingFlags.SetField, null, this, new Object[] {new AccessLevel()});
            }

        }

        public string Serialize()
        {
            string result = "";

            Type r_type = typeof(Rights);

            FieldInfo[] fi_array = r_type.GetFields();

            foreach (FieldInfo fi in fi_array)
            {
                AccessLevel al = (AccessLevel)r_type.InvokeMember(fi.Name, BindingFlags.GetField, null, this,null);
                if(al.read)
                    result += "1";
                else
                    result += "0";

                if (al.write)
                    result += "1";
                else
                    result += "0";
            }

            return result;
        }

        public void Deserialize(string r_string)
        {
            Type r_type = typeof(Rights);

            FieldInfo[] fi_array = r_type.GetFields();

            int index = 0;

            foreach (FieldInfo fi in fi_array)
            {
                try
                {
                    AccessLevel al = new AccessLevel();
                    if (r_string[index++] == '1')
                        al.read = true;
                    else
                        al.read = false;

                    if (r_string[index++] == '1')
                        al.write = true;
                    else
                        al.write = false;

                    r_type.InvokeMember(fi.Name, BindingFlags.SetField, null, this, new Object[] { al });
                }
                catch (Exception)
                {
                    //Длины строки не хватило на все права
                    r_type.InvokeMember(fi.Name, BindingFlags.SetField, null, this, new Object[] { new AccessLevel() });            
                }
                            
            }
        }
    }

    class AuthModule
    {
        public static int user_id;
        public static Rights rights;
        static AuthModule()
        {
            rights = new Rights();
        }
    }
}
