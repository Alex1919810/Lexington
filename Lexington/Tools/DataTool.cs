using Lexington.Model;

namespace Lexington.Tools
{
    public static class DataTool
    {
        public static int CountActiveMatter()
        {
            int count = 0;
            for (int i = 0; i < GlobalValue.Matters.Count; i++)
            {
                var matter = GlobalValue.Matters[i];
                if (matter.IsRunning == false) continue;
                if (DateTime.Now >= matter.EndTime)
                {
                    PushMesToQue(0, i);
                    if (matter.IsRedo)
                    {
                        var currentTime = DateTime.Now.AddSeconds(-DateTime.Now.Second);
                        TimeSpan tmpTime = matter.EndTime.Value - matter.StartTime.Value;
                        matter.StartTime = DateTime.Now;
                        matter.EndTime = DateTime.Now + tmpTime;
                        matter.IsRunning = true;
                        matter.Process = 0;
                    }
                    else
                    {
                        matter.IsRunning = false;
                        matter.Process = 100;
                    }
                }
                if (matter.IsRunning == true) count++;
            }
            return count;
        }

        public static double CalculateProcess(DateTime? StartTime, DateTime? EndTime)
        {
            double process = 0;
            TimeSpan totalTimeRange = EndTime.Value - StartTime.Value;
            TimeSpan elapsedTime = DateTime.Now - StartTime.Value;
            process = (elapsedTime.TotalMilliseconds / totalTimeRange.TotalMilliseconds) * 100;
            return process;
        }

        public static string ExpenditionDialog(Matter matter)
        {
            string s = string.Empty;
            ResoureCount resoureCount = FilesTool.ParseExpeditionResource(matter.MatterName);
            if (DateTime.Now.Day == matter.EndTime.Value.Day)
            {
                GlobalValue.ResourceGetTotal += resoureCount;
                s += "司令官,远征" + matter.MatterName + string.Format("任务结束了,今天我们至少收获了{0}燃油,{1}弹药,{2}钢铁,{3}铝材,{4}个快修,{5}个快建,{6}张舰船图纸,{7}张装备图纸。",
                GlobalValue.ResourceGetTotal.Oil, GlobalValue.ResourceGetTotal.Munition, GlobalValue.ResourceGetTotal.Steel, GlobalValue.ResourceGetTotal.Aluminium,
                GlobalValue.ResourceGetTotal.QuickConstruction, GlobalValue.ResourceGetTotal.RapidRepair, GlobalValue.ResourceGetTotal.ShipBlueprint, GlobalValue.ResourceGetTotal.EquipmentBlueprint);
            }
            else
            {
                s += "司令官,远征" + matter.MatterName + "任务在" + matter.EndTime.Value.Day + "号已经结束了，有好好记得收远征吗？";
            }

            return s;
        }

        public static string NormalMatterDialog(Matter matter)
        {
            string s = string.Empty;
            if (DateTime.Now.Day == matter.EndTime.Value.Day)
            {
                s += "司令官,任务" + matter.MatterName + "的提醒时间到了，请不要忘记了。";
            }
            else
            {
                s += "司令官,任务" + matter.MatterName + "在" + matter.EndTime.Value.Day + "号已经结束了，但愿您没有错过。";
            }

            return s;
        }

        public static void PushMesToQue(int Type = 0, int Index = 0)
        {
            GlobalValue.MesQue.Enqueue(new GlobalValue.Mes { type = Type, index = Index });
            GlobalValue.MesSemaphore.Release();
        }

        public static long GetTimeInSecond(string formatTime)
        {
            long totalMilliSecends = 0;

            if (!string.IsNullOrEmpty(formatTime))
            {
                string[] timeParts = formatTime.Split(':');
                totalMilliSecends = Convert.ToInt16(timeParts[0]) * 60 * 60
                    + Convert.ToInt16(timeParts[1]) * 60
                    + (long)Math.Round(double.Parse(timeParts[2]));
            }

            return totalMilliSecends;
        }
    }


}
