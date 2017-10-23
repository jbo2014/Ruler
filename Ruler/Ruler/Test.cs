using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Noesis.Javascript;

namespace Ruler
{
    public class Test
    {

        public object aa()
        {
            using (JavascriptContext ctx = new JavascriptContext())
            {
                var i = ctx.Run("1+2");
                Console.WriteLine(i);//3
                ctx.Run("var d = new Date(2013,9-1,2,20,30,15)");
                var d = ctx.GetParameter("d");
                Console.WriteLine(d);//2013/9/2 20:30:15
                var j = ctx.Run("function add(x,y){return x+y;};add(5,10);");
                Console.WriteLine(j);//15
                ctx.Run("var obj = {};obj.name='jimmy';obj.sex='Male';obj.name='杨俊明'");
                var obj = ctx.GetParameter("obj") as Dictionary<string, object>;
                foreach (var key in obj.Keys)
                {
                    Console.WriteLine(string.Format("{0}:{1}", key, obj[key]));
                    //name:杨俊明
                    //sex:Male
                }
                var jsonArr = ctx.Run("[{Airport:'PEK',Name:'北京首都机场'},{Airport:'XIY',Name:'西安咸阳机场'}]") as Array;
                foreach (var item in jsonArr)
                {
                    var json = item as Dictionary<string, object>;
                    foreach (var key in json.Keys)
                    {
                        Console.WriteLine(string.Format("{0}:{1}", key, json[key]));
                        //Airport:PEK
                        //Name:北京首都机场
                        //Airport:XIY
                        //Name:西安咸阳机场
                    }
                }
                ctx.SetParameter("x", 7);
                ctx.Run("function add(x,y){return x+y;}; function sub(x,y){return x-y;};k = add(2,x);var t = sub(9,1);");
                var k = ctx.GetParameter("k");
                Console.WriteLine(k);//9
                var t = ctx.GetParameter("t");
                return t;
            }

        }
    }
}
