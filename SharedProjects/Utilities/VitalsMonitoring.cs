using System;
using System.Collections.Generic;
using System.Linq;
using SharedProjects.Models;

namespace SharedProjects.Utilities
{
    public class VitalsMonitoring
    {
        readonly ConfigDbContext _context = new ConfigDbContext();

        public Alarm GetVitalsForSpecificPatient(int id)
        {
            var vitalStore = _context.VitalsLogs.ToList();
            var vitals = vitalStore.Where(item => item.PatientId == id).ToList();
            Alarm alarms = new Alarm
            {
                Messages = new List<string>()
            };
            foreach (VitalsLogs log in vitals.Skip(Math.Max(0, vitals.Count - 10)))
            {
                var pid = log.PatientId;
                alarms.Id = pid;
                var patient = _context.Patients.FirstOrDefault(item => item.PatientId == pid);
                alarms.Name = patient?.PatientName;
                int[] arr = new int[3];
                arr[0] = CheckSpo2(log.Spo2Rate);
                arr[1] = CheckBpm(log.BpmRate);
                arr[2] = CheckRespRate(log.RespRate);
                int count = CountZeroes(arr);
                if (count!=3)
                {
                    var tempMsg = "LogId : " + log.VitalsLogId + ", "  + "SPO2 : " + InterpretMessage(arr[0]) + ", " + "BPM : " + InterpretMessage(arr[1]) + ", " + "RespRate : " + InterpretMessage(arr[2]);
                    alarms.Messages.Add(tempMsg);
                }

            }
            return alarms;
        }

        public int CountZeroes(int [] arr)
        {
            int ctr = 0;
            foreach (int i in arr)
            {
                if (i == 0)
                    ctr++;
            }

            return ctr;
        }

        public string CheckVitals(VitalsLogs vital)
        {
            var pid = vital.PatientId;
            var patient = _context.Patients.FirstOrDefault(item => item.PatientId == pid);
            var pname = patient?.PatientName;
            var spo2 = CheckSpo2(vital.Spo2Rate);
            var bpm = CheckBpm(vital.BpmRate);
            var respRate = CheckRespRate(vital.RespRate);
            var a = "Spo2 Rate " + InterpretMessage(spo2);
            var b = "Bpm Rate " + InterpretMessage(bpm);
            var c = "Respiratory Rate " + InterpretMessage(respRate);
            var s = "" + pid + "," + pname + "," + a + "," + b + "," + c;
            return s;
        }
        public int CheckSpo2(double spo2)
        {
            if (spo2 < 95)
            {
                return -1;
            }
            else
                return 0;

        }
        public int CheckBpm(double bpm)
        {
            if (bpm < 70)
                return -1;
            if (bpm > 100)
                return 1;
            return 0;
        }
        public int CheckRespRate(double respRate)
        {
            if (respRate < 12)
                return -1;
            if (respRate > 16)
                return 1;
            return 0;
        }

        public string InterpretMessage(int msg)
        {
            if (msg == -1)
                return "is low";
            if (msg == 1)
                return "is high";
            return "is good";
        }
    }
}
