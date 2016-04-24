using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
    public class Itemset
    {
        public int count { get; set; }
        public string Item { get; set; }
       
        public Itemset()
        {
          
            
        }
        public Itemset(string itemset) 
        {
            Item = itemset;
            count = 0;
            Lists.sets.Add(this);
        }



        //ایا یک رشته داخل رشته ی دیگر وجود دارد یا نه
        public bool Is (string smal,string larg)
        {
            if(smal[0]=='{')
            smal = smal.Substring(1, smal.Length - 2);
            if(larg[0]=='{')
            larg = larg.Substring(1, larg.Length - 2);
            bool flag=false;
            string[] str = smal.Split(';');
            string[] str2 = larg.Split(';');
            for (int i = 0; i < str.Length; i++)
            {
                flag = false;
                for (int j = 0; j < str2.Length; j++)
                {
                    if (str[i] == str2[j])
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                {
                    return false;
                }
            }
            return true;
        }

        //شمردن تعداد یه مجموعه داده در کل مجموعه مون
        public void count1(List<string> list)
        {
            for(int  i=0;i<Lists.sets.Count;i++)
                foreach(string j in list)
                {
                    if(Is(Lists.sets[i].Item,j)==true)
                      Lists.sets[i].count++;
                }
        }




        //مجموعه مون اول کار
        public  void first(List<string> list)
        {
            List<string> l = new List<string>();
            for (int i = 0; i < list.Count;i++ )
            {
                string str = list[i];
                if (str[0] == '{')
                    str = str.Substring(1, str.Length - 2);
                string[] s = str.Split(';');
                foreach (string y in s)
                    if (l.Contains(y) == false)
                        l.Add(y);

            }

            Itemset ll;
            for (int h = 0; h < l.Count; h++)
                ll = new Itemset(l[h]);

            
        }
       
        //تابعی ک دو رشته را به هم الحاق میکند
        public string subjoin(string str1,string str2)
        {
            if(str1[0]=='{')
            str1 = str1.Substring(1, str1.Length - 2);
            if (str2[0] == '{')
            str2 = str2.Substring(1, str2.Length - 2);
            string[] s = str1.Split(';');
            string[] s2 = str2.Split(';');
            string str="";
            bool flag=false;
            for(int i=0;i<s.Length;i++)
            {
                flag = false;
                for(int j=0;j<s2.Length;j++)
                {
                    if (s[i] == s2[j])
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                    str += s[i] + ";";

            }
            for(int i=0;i<s2.Length;i++)
                str += s2[i] + ";";

            str = str.Substring(0, str.Length - 1);

            return str;
        }



        //تابعی ک دو مجموعه را با هم جوین میکنه
        public void join (int degree)
        {
            List<string> list = new List<string>();
            for(int i=0;i<Lists.sets.Count;i++)
            {
                for(int j=i;j<Lists.sets.Count;j++)
                {
                    list.Add(subjoin(Lists.sets[i].Item, Lists.sets[j].Item));
                    
                }
            }
            List<string> finallist = new List<string>();
            for (int k = 0; k < list.Count;k++ )
            {
                string[] s = list[k].Split(';');
                if (s.Length == degree)
                    finallist.Add(list[k]);
            }
            
            for (int h = 0; h < finallist.Count;h++)
            {
                for(int l=h+1;l<finallist.Count;l++)
                {
                    if (Equal(finallist[h], finallist[l]))
                    {
                        finallist.Remove(finallist[h]);
                        l--;
                    }

                }
            }
                Lists.sets.Clear();
            
            foreach(string i in finallist)
            {
                Itemset j = new Itemset(i);
            }
        }


        //برای پاک کردن ایتم ست های مساوی
        public bool Equal(string str1,string str2)
        {
            bool flag = false;
            if (str1[0] == '{')
                str1 = str1.Substring(1, str1.Length - 2);
            if (str2[0] == '{')
                str2 = str2.Substring(1, str2.Length - 2);
            string[] s1 = str1.Split(';');
            string[] s2 = str2.Split(';');
            if (s1.Length != s2.Length)
                return false;
            else
            {
                for(int i=0;i<s1.Length;i++)
                {
                    flag=false;
                    for(int j=0;j<s2.Length;j++)
                    {
                        if(s1[i]==s2[j])
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag == false)
                        return false;
                }
                return true;
            }

        }

        //الگوی یافته شده را برگرداند
        public List<string> patern(List<string> list, int minsup,List<string> list1)
        {
            List<string> list2 = new List<string>();
            first(list1);
            int k=2;
            foreach(Itemset i in Lists.sets)
                list2.Add(i.Item);
            while(true)
            {

                count1(list);
                List<Itemset> set = new List<Itemset>();
                foreach(Itemset i in Lists.sets)
                    if(i.count>=minsup)
                    {
                        Itemset kk = new Itemset();
                        kk.Item = i.Item;
                        kk.count = i.count;
                        set.Add(kk);
                        
                    }
                Lists.sets.Clear();
                foreach (Itemset i in set)
                    Lists.sets.Add(i);
                if(Lists.sets.Count!=0)
                {
                    list2.Clear();
                    foreach (Itemset j in Lists.sets)
                        list2.Add(j.Item);
                }


                if (Lists.sets.Count == 0)
                    break;
                join(k);
                k++;

            }

            return list2;
        }
       
    }
    
}
