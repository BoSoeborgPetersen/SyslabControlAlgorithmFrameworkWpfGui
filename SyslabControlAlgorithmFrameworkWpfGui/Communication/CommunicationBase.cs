using SyslabControlAlgorithmFrameworkWpfGui.Model.Devices;
using SyslabControlAlgorithmFrameworkWpfGui.Serialization;
using System;
using System.Collections.Generic;

namespace SyslabControlAlgorithmFrameworkWpfGui.Communication
{
    public abstract class CommunicationBase
    {
        protected bool talkToJava = true;

        protected int GetSerializationNumber()//SerializationType serializationType)
        {
            return 10;
            //int i = 0;
            //foreach (SerializationType type in Enum.GetValues(typeof(SerializationType)))
            //{
            //    if (type.Equals(serializationType))
            //        return i;
            //    i++;
            //}
            //throw new Exception("Invalid serialization type");
        }

        protected byte[] AppendSerializationNumber(byte[] serialized, int serializationNumber)
        {
            Array.Resize(ref serialized, serialized.Length + 1);
            serialized[serialized.Length] = IntToByte(serializationNumber);
            return serialized;
        }

        protected int ExtractSerializationNumber(byte[] serialized) => ByteToInt(serialized[serialized.Length - 1]);

        protected byte[] RemoveSerializationNumber(byte[] serialized)
        {
            Array.Resize(ref serialized, serialized.Length - 1);
            return serialized;
        }

        protected static int ByteToInt(byte b) => b & 0xFF;

        protected static byte IntToByte(int a) => (byte)(a & 0xFF);

        protected string AppendSerializationNumber(string serialized, int serializationNumber)
        {
            string serializationNumberstring = serializationNumber > 9 ? "" + serializationNumber : "0" + serializationNumber;
            return serialized + serializationNumberstring;
        }

        protected int ExtractSerializationNumber(string serialized)
        {
            string serializationNumberstring = serialized.Substring(serialized.Length - 2, 2);
            return int.Parse(serializationNumberstring);
        }

        protected string RemoveSerializationNumber(string serialized) => serialized.Substring(0, serialized.Length - 2);

        protected string ScrambleEscapedXmlTags(string xml) => xml.Replace("&", "@");

        protected string UnscrambleEscapedXmlTags(string xml) => xml.Replace("@", "&");

        protected string MaybeConvertToJavaType(string name)
        {
            if (!talkToJava) return name;

            if (name == "String") return "java.lang.String";
            if (name == "Double") return "java.lang.Double";
            if (name == "Boolean") return "java.lang.Boolean";
            if (name == "Object[]") return "[Ljava.lang.Object;";
            else
                return null;
        }

        protected string MaybeConvertFromJavaType(string name)
        {
            if (!talkToJava) return name;

            // Collections.
            if (name == "java.util.ArrayList") return "System.Collections.ArrayList";
            if (name == "java.util.Vector") return "System.Collections.ArrayList";

            // Value types and string.
            if (name == "java.lang.Double") return "System.Double";
            if (name == "java.lang.Integer") return "System.Int32";
            if (name == "java.lang.Boolean") return "System.Boolean";
            if (name == "java.lang.String") return "System.String";

            // Arrays of Value types and string.
            if (name == "[Ljava.lang.Double;") return "System.Collections.Generic.List`1[System.Double]";
            if (name == "[Ljava.lang.Integer;") return "System.Collections.Generic.List`1[System.Int32]";
            if (name == "[Ljava.lang.Boolean;") return "System.Collections.Generic.List`1[System.Boolean]";
            if (name == "[Ljava.lang.String;") return "System.Collections.Generic.List`1[System.String]";

            // Model classes.
            if (name == "risoe.syslab.model.Composite2DArray") return "SyslabControlAlgorithmFrameworkWpfGui.Model.Composite2DArray";
            if (name == "risoe.syslab.model.CompositeArray") return "SyslabControlAlgorithmFrameworkWpfGui.Model.CompositeArray";
            if (name == "risoe.syslab.model.CompositeBoolean") return "SyslabControlAlgorithmFrameworkWpfGui.Model.CompositeBoolean";
            if (name == "risoe.syslab.model.CompositeInteger") return "SyslabControlAlgorithmFrameworkWpfGui.Model.CompositeInteger";
            if (name == "risoe.syslab.model.CompositeMeasurement") return "SyslabControlAlgorithmFrameworkWpfGui.Model.CompositeMeasurement";
            if (name == "risoe.syslab.model.CompositeStatus") return "SyslabControlAlgorithmFrameworkWpfGui.Model.CompositeStatus";
            if (name == "risoe.syslab.model.CompositeString") return "SyslabControlAlgorithmFrameworkWpfGui.Model.CompositeString";
            if (name == "risoe.syslab.model.CompositeTimeseries") return "SyslabControlAlgorithmFrameworkWpfGui.Model.CompositeTimeseries";
            if (name == "risoe.syslab.model.MeasurementItem") return "SyslabControlAlgorithmFrameworkWpfGui.Model.MeasurementItem";
            if (name == "risoe.syslab.model.MeasurementName") return "SyslabControlAlgorithmFrameworkWpfGui.Model.MeasurementName";

            // IEC Model classes.
            if (name == "risoe.syslab.model.iec61850.ALRM") return "SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.ALRM";
            if (name == "risoe.syslab.model.iec61850.COMX") return "SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.COMX";
            if (name == "risoe.syslab.model.iec61850.DELV") return "SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.DELV";
            if (name == "risoe.syslab.model.iec61850.EVNT") return "SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.EVNT";
            if (name == "risoe.syslab.model.iec61850.GPSL") return "SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.GPSL";
            if (name == "risoe.syslab.model.iec61850.HLTH") return "SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.HLTH";
            if (name == "risoe.syslab.model.iec61850.LNPL") return "SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.LNPL";
            if (name == "risoe.syslab.model.iec61850.PNPL") return "SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.PNPL";
            if (name == "risoe.syslab.model.iec61850.TSTP") return "SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.TSTP";
            if (name == "risoe.syslab.model.iec61850.WYEA") return "SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.WYEA";
            if (name == "risoe.syslab.model.iec61850.WYEV") return "SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.WYEV";

            // Arrays of IEC Model classes.
            if (name == "[Lrisoe.syslab.model.iec61850.ALRM;") return "System.Collections.Generic.List`1[SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.ALRM]";
            if (name == "[Lrisoe.syslab.model.iec61850.COMX;") return "System.Collections.Generic.List`1[SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.COMX]";
            if (name == "[Lrisoe.syslab.model.iec61850.DELV;") return "System.Collections.Generic.List`1[SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.DELV]";
            if (name == "[Lrisoe.syslab.model.iec61850.EVNT;") return "System.Collections.Generic.List`1[SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.EVNT]";
            if (name == "[Lrisoe.syslab.model.iec61850.GPSL;") return "System.Collections.Generic.List`1[SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.GPSL]";
            if (name == "[Lrisoe.syslab.model.iec61850.HLTH;") return "System.Collections.Generic.List`1[SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.HLTH]";
            if (name == "[Lrisoe.syslab.model.iec61850.LNPL;") return "System.Collections.Generic.List`1[SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.LNPL]";
            if (name == "[Lrisoe.syslab.model.iec61850.PNPL;") return "System.Collections.Generic.List`1[SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.PNPL]";
            if (name == "[Lrisoe.syslab.model.iec61850.TSTP;") return "System.Collections.Generic.List`1[SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.TSTP]";
            if (name == "[Lrisoe.syslab.model.iec61850.WYEA;") return "System.Collections.Generic.List`1[SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.WYEA]";
            if (name == "[Lrisoe.syslab.model.iec61850.WYEV;") return "System.Collections.Generic.List`1[SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850.WYEV]";

            // Device Model classes.
            if (name == "risoe.syslab.model.devices.BattOpMode") return "SyslabControlAlgorithmFrameworkWpfGui.Model.Devices.BattOpMode";
            if (name == "risoe.syslab.model.devices.ConnType") return "SyslabControlAlgorithmFrameworkWpfGui.Model.Devices.ConnType";
            if (name == "risoe.syslab.model.devices.CostFunction") return "SyslabControlAlgorithmFrameworkWpfGui.Model.Devices.CostFunction";
            if (name == "risoe.syslab.model.devices.FlowBatteryPumpState") return "SyslabControlAlgorithmFrameworkWpfGui.Model.Devices.FlowBatteryPumpState";
            if (name == "risoe.syslab.model.devices.FlowBatteryState") return "SyslabControlAlgorithmFrameworkWpfGui.Model.Devices.FlowBatteryState";
            if (name == "risoe.syslab.model.devices.GMode") return "SyslabControlAlgorithmFrameworkWpfGui.Model.Devices.GMode";
            if (name == "risoe.syslab.model.devices.LithiumBatteryState") return "SyslabControlAlgorithmFrameworkWpfGui.Model.Devices.LithiumBatteryState";
            if (name == "risoe.syslab.model.devices.LoadOpMode") return "SyslabControlAlgorithmFrameworkWpfGui.Model.Devices.LoadOpMode";
            if (name == "risoe.syslab.model.devices.LookupTable") return "SyslabControlAlgorithmFrameworkWpfGui.Model.Devices.LookupTable";
            if (name == "risoe.syslab.model.devices.PVInverterState") return "SyslabControlAlgorithmFrameworkWpfGui.Model.Devices.PVInverterState";
            if (name == "risoe.syslab.model.devices.PVOpMode") return "SyslabControlAlgorithmFrameworkWpfGui.Model.Devices.PVOpMode";
            if (name == "risoe.syslab.model.devices.RMode") return "SyslabControlAlgorithmFrameworkWpfGui.Model.Devices.RMode";
            if (name == "risoe.syslab.model.devices.SignalResponse") return "SyslabControlAlgorithmFrameworkWpfGui.Model.Devices.SignalResponse";
            if (name == "risoe.syslab.model.devices.Timetable") return "SyslabControlAlgorithmFrameworkWpfGui.Model.Devices.Timetable";
            if (name == "risoe.syslab.model.devices.WindTurbineState") return "SyslabControlAlgorithmFrameworkWpfGui.Model.Devices.WindTurbineState";

            // Arrays of Device Model classes.
            if (name == "[Lrisoe.syslab.model.devices.GMode;") return "System.Collections.Generic.List`1[SyslabControlAlgorithmFrameworkWpfGui.Model.Devices.GMode]";
            if (name == "[Lrisoe.syslab.model.devices.RMode;") return "System.Collections.Generic.List`1[SyslabControlAlgorithmFrameworkWpfGui.Model.Devices.RMode]";
            if (name == "[Lrisoe.syslab.model.devices.LoadOpMode;") return "System.Collections.Generic.List`1[SyslabControlAlgorithmFrameworkWpfGui.Model.Devices.LoadOpMode]";

            // Control Algorithm Framework model classes.
            if (name == "risoe.syslab.model.CapabilityDescription") return "SyslabControlAlgorithmFrameworkWpfGui.Model.CapabilityDescription";
            if (name == "risoe.syslab.model.RequirementDescription") return "SyslabControlAlgorithmFrameworkWpfGui.Model.RequirementDescription";
            if (name == "risoe.syslab.model.Flexibility") return "SyslabControlAlgorithmFrameworkWpfGui.Model.Flexibility";
            if (name == "risoe.syslab.model.flexibility.FlexibilityExecutionsLoadShifting") return "SyslabControlAlgorithmFrameworkWpfGui.Model.FlexibilityExecutions";
            else
                return null;
        }
    }
}
