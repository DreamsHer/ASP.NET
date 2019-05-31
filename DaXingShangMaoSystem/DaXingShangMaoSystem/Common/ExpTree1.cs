using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;

namespace System.Linq
{
    public static class ExpTree1
    {
        public static string GetJSONTreeData1<T>(this IEnumerable<T> list, string ChildIDName, string ChildPropertyName,string FatherIDName)
        {
            //把T转化为TreeStrut；
            List<TreeStrut1> listTreeStrut = ConvertType(list, ChildIDName, ChildPropertyName, FatherIDName);
            string strJson = GetNewGoodsManageTree(listTreeStrut, 0);
            return strJson;
        }
        //类型转化
        public static List<TreeStrut1> ConvertType<T>(IEnumerable<T> list, string ChildIDName, string ChildPropertyName, string FatherIDName)
        {
            List<TreeStrut1> listTreeStrut = new List<TreeStrut1>();
            foreach (var item in list)
            {
                TreeStrut1 ts = new TreeStrut1();
                PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(item);
                PropertyDescriptor pd = pdc.Find(ChildIDName, false);
                ts.id = Convert.ToInt32(pd.GetValue(item).ToString());
                pd = pdc.Find(ChildPropertyName, false);
                ts.text = pd.GetValue(item).ToString();
                pd = pdc.Find(FatherIDName, false);
                ts.Fid = Convert.ToInt32(pd.GetValue(item).ToString());
                listTreeStrut.Add(ts);
            }
            return listTreeStrut;
        }

        //把list结构转化为json字符串
        public static string GetNewGoodsManageTree(List<TreeStrut1> list, int Fid)
        {
            StringBuilder sbTree = new StringBuilder();
            List<TreeStrut1> listNode1 = list.Where(m => m.Fid == Fid).ToList();
            if (listNode1.Count > 0)
            {
                //有节点存在
                sbTree.Append("[");
                for (int i = 0; i < listNode1.Count; i++)
                {
                    //获取子节点
                    int proid = listNode1[i].id;
                    //判断当前节点是否有子节点
                    string sbChild = GetNewGoodsManageTree(list, proid);

                    //获取json格式
                    if (sbChild.ToString() != "")
                    {
                        sbTree.Append("{\"id\":\"" + listNode1[i].id + "\",\"text\":\"" + listNode1[i].text + "\",\"children\":");
                        sbTree.Append(sbChild);
                        sbTree.Append("},");
                    }
                    else
                    {
                        sbTree.Append("{\"id\":\"" + listNode1[i].id + "\",\"text\":\"" + listNode1[i].text + "\"},");
                    }
                }
                //没有子节点               
                sbTree.Remove(sbTree.Length - 1, 1);
                sbTree.Append("]");
            }
            return sbTree.ToString();
        }

        public class TreeStrut1
        {
            public int id { get; set; }
            public string text { get; set; }
            public int Fid { get; set; }
        }
    }
}