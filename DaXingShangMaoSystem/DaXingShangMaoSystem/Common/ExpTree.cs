using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;

namespace System.Linq
{
    public static class ExpTree
    {
        public static string GetJSONTreeData<T>(this IEnumerable<T> list, string ChildIDPropertyName, string ChildNamePropertyName,string FatherIDPropertyName)
        {
            //把T转化为TreeStrut；
            List<TreeStrut> listTreeStrut = ConvertType(list, ChildIDPropertyName, ChildNamePropertyName, FatherIDPropertyName);
            string strJson = GetDrugTypeTreeData(listTreeStrut, 0);
            return strJson;
        }
        //类型转化
        public static List<TreeStrut> ConvertType<T>(IEnumerable<T> list, string ChildIDPropertyName, string ChildNamePropertyName, string FatherIDPropertyName)
        {
            List<TreeStrut> listTreeStrut = new List<TreeStrut>();
            foreach (var item in list)
            {
                TreeStrut ts = new TreeStrut();
                PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(item);
                PropertyDescriptor pd = pdc.Find(ChildIDPropertyName, false);
                ts.id = Convert.ToInt32(pd.GetValue(item).ToString());
                pd = pdc.Find(ChildNamePropertyName, false);
                ts.text = pd.GetValue(item).ToString();
                //pd = pdc.Find(ChildNamePropertyCode, false);
                //ts.text = pd.GetValue(item).ToString();
                pd = pdc.Find(FatherIDPropertyName, false);
                ts.fid = Convert.ToInt32(pd.GetValue(item).ToString());
                listTreeStrut.Add(ts);
            }
            return listTreeStrut;
        }

        //把list结构转化为json字符串
        public static string GetDrugTypeTreeData(List<TreeStrut> list, int fid)
        {        
            StringBuilder sbTree = new StringBuilder();
            List<TreeStrut> listNode = list.Where(m => m.fid == fid).ToList();
            if (listNode.Count > 0)
            {
                //有节点存在
                sbTree.Append("[");
                for (int i = 0; i < listNode.Count; i++)
                {
                    //获取子节点
                    int proid = listNode[i].id;
                    //判断当前节点是否有子节点
                    string sbChild = GetDrugTypeTreeData(list, proid);                               

                    //获取json格式
                    if (sbChild.ToString() != "")
                    {
                        sbTree.Append("{\"id\":\"" + listNode[i].id + "\",\"text\":\"" + listNode[i].text + "\",\"children\":");
                        sbTree.Append(sbChild);
                        sbTree.Append("},");
                    }
                    else
                    {
                        sbTree.Append("{\"id\":\"" + listNode[i].id + "\",\"text\":\"" + listNode[i].text + "\"},");
                    }                   
                }
                //没有子节点               
                sbTree.Remove(sbTree.Length - 1, 1);
                sbTree.Append("]");
            }
            return sbTree.ToString();
        }
    }

    public class TreeStrut
    {
        public int id { get; set; }
        public string text { get; set; }
        public int fid { get; set; }
    }
}