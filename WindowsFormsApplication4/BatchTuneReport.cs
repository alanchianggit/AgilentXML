﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.6.1055.0.
// 
namespace BatchTuneReport
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "TuneReport")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "TuneReport", IsNullable = false)]
    public partial class TuneReportDataSet
    {

        private TuneReportDataSetTuneReport[] tuneReportField;

        private TuneReportDataSetTuneReportItem[] tuneReportItemField;

        private TuneReportDataSetTuneElement[] tuneElementField;

        private TuneReportDataSetTuneParameter[] tuneParameterField;

        private string schemaVersionField;

        private string dataVersionField;

        private string sIVersionField;

        private string auditTrailField;

        private string batchNameField;

        private string batchDataPathField;

        private string qCDataPathField;

        private string hashCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TuneReport")]
        public TuneReportDataSetTuneReport[] TuneReport
        {
            get
            {
                return this.tuneReportField;
            }
            set
            {
                this.tuneReportField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TuneReportItem")]
        public TuneReportDataSetTuneReportItem[] TuneReportItem
        {
            get
            {
                return this.tuneReportItemField;
            }
            set
            {
                this.tuneReportItemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TuneElement")]
        public TuneReportDataSetTuneElement[] TuneElement
        {
            get
            {
                return this.tuneElementField;
            }
            set
            {
                this.tuneElementField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TuneParameter")]
        public TuneReportDataSetTuneParameter[] TuneParameter
        {
            get
            {
                return this.tuneParameterField;
            }
            set
            {
                this.tuneParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SchemaVersion
        {
            get
            {
                return this.schemaVersionField;
            }
            set
            {
                this.schemaVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DataVersion
        {
            get
            {
                return this.dataVersionField;
            }
            set
            {
                this.dataVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SIVersion
        {
            get
            {
                return this.sIVersionField;
            }
            set
            {
                this.sIVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AuditTrail
        {
            get
            {
                return this.auditTrailField;
            }
            set
            {
                this.auditTrailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BatchName
        {
            get
            {
                return this.batchNameField;
            }
            set
            {
                this.batchNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BatchDataPath
        {
            get
            {
                return this.batchDataPathField;
            }
            set
            {
                this.batchDataPathField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string QCDataPath
        {
            get
            {
                return this.qCDataPathField;
            }
            set
            {
                this.qCDataPathField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string HashCode
        {
            get
            {
                return this.hashCodeField;
            }
            set
            {
                this.hashCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "TuneReport")]
    public partial class TuneReportDataSetTuneReport
    {

        private string tuneReportIDField;

        private string reportSetIDField;

        private string versionField;

        private string createdField;

        private string tuneModeField;

        private string nebulizerTypeField;

        private string torchTypeField;

        private string checkOutField;

        private string modelNameField;

        private string serialNumberField;

        private string tuneReportNumberField;

        private string tuneStepNumberField;

        private string reportCommentField;

        /// <remarks/>
        public string TuneReportID
        {
            get
            {
                return this.tuneReportIDField;
            }
            set
            {
                this.tuneReportIDField = value;
            }
        }

        /// <remarks/>
        public string ReportSetID
        {
            get
            {
                return this.reportSetIDField;
            }
            set
            {
                this.reportSetIDField = value;
            }
        }

        /// <remarks/>
        public string Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        public string Created
        {
            get
            {
                return this.createdField;
            }
            set
            {
                this.createdField = value;
            }
        }

        /// <remarks/>
        public string TuneMode
        {
            get
            {
                return this.tuneModeField;
            }
            set
            {
                this.tuneModeField = value;
            }
        }

        /// <remarks/>
        public string NebulizerType
        {
            get
            {
                return this.nebulizerTypeField;
            }
            set
            {
                this.nebulizerTypeField = value;
            }
        }

        /// <remarks/>
        public string TorchType
        {
            get
            {
                return this.torchTypeField;
            }
            set
            {
                this.torchTypeField = value;
            }
        }

        /// <remarks/>
        public string CheckOut
        {
            get
            {
                return this.checkOutField;
            }
            set
            {
                this.checkOutField = value;
            }
        }

        /// <remarks/>
        public string ModelName
        {
            get
            {
                return this.modelNameField;
            }
            set
            {
                this.modelNameField = value;
            }
        }

        /// <remarks/>
        public string SerialNumber
        {
            get
            {
                return this.serialNumberField;
            }
            set
            {
                this.serialNumberField = value;
            }
        }

        /// <remarks/>
        public string TuneReportNumber
        {
            get
            {
                return this.tuneReportNumberField;
            }
            set
            {
                this.tuneReportNumberField = value;
            }
        }

        /// <remarks/>
        public string TuneStepNumber
        {
            get
            {
                return this.tuneStepNumberField;
            }
            set
            {
                this.tuneStepNumberField = value;
            }
        }

        /// <remarks/>
        public string ReportComment
        {
            get
            {
                return this.reportCommentField;
            }
            set
            {
                this.reportCommentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "TuneReport")]
    public partial class TuneReportDataSetTuneReportItem
    {

        private string tuneReportIDField;

        private string reportSetIDField;

        private string reportItemIDField;

        private string tuneItemTypeField;

        private string integTimeField;

        private string acquisitionTimeField;

        private string samplingPeriodField;

        /// <remarks/>
        public string TuneReportID
        {
            get
            {
                return this.tuneReportIDField;
            }
            set
            {
                this.tuneReportIDField = value;
            }
        }

        /// <remarks/>
        public string ReportSetID
        {
            get
            {
                return this.reportSetIDField;
            }
            set
            {
                this.reportSetIDField = value;
            }
        }

        /// <remarks/>
        public string ReportItemID
        {
            get
            {
                return this.reportItemIDField;
            }
            set
            {
                this.reportItemIDField = value;
            }
        }

        /// <remarks/>
        public string TuneItemType
        {
            get
            {
                return this.tuneItemTypeField;
            }
            set
            {
                this.tuneItemTypeField = value;
            }
        }

        /// <remarks/>
        public string IntegTime
        {
            get
            {
                return this.integTimeField;
            }
            set
            {
                this.integTimeField = value;
            }
        }

        /// <remarks/>
        public string AcquisitionTime
        {
            get
            {
                return this.acquisitionTimeField;
            }
            set
            {
                this.acquisitionTimeField = value;
            }
        }

        /// <remarks/>
        public string SamplingPeriod
        {
            get
            {
                return this.samplingPeriodField;
            }
            set
            {
                this.samplingPeriodField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "TuneReport")]
    public partial class TuneReportDataSetTuneElement
    {

        private string tuneReportIDField;

        private string reportSetIDField;

        private string reportItemIDField;

        private string tuneElementIDField;

        private string mzField;

        private string heightField;

        private string axisField;

        private string width10Field;

        private string width10PosXField;

        private string width10PosYField;

        private string width50Field;

        private string width50PosXField;

        private string width50PosYField;

        private string peakHeightPercentField;

        private string widthField;

        private string widthPosXField;

        private string widthPosYField;

        private string countField;

        private string rsdField;

        private string responseRatioField;

        private string cpsField;

        private string sampleConcField;

        private string isReferenceField;

        private string plotRangeYAxisField;

        private string convertedXValuesField;

        private string convertedYValuesField;

        private string axisCheckPassField;

        private string axisCheckMinField;

        private string axisCheckMaxField;

        private string widthCheckPassField;

        private string widthCheckMaxField;

        private string responseCheckPassField;

        private string responseCheckMinField;

        private string rsdCheckPassField;

        private string rsdCheckMaxField;

        /// <remarks/>
        public string TuneReportID
        {
            get
            {
                return this.tuneReportIDField;
            }
            set
            {
                this.tuneReportIDField = value;
            }
        }

        /// <remarks/>
        public string ReportSetID
        {
            get
            {
                return this.reportSetIDField;
            }
            set
            {
                this.reportSetIDField = value;
            }
        }

        /// <remarks/>
        public string ReportItemID
        {
            get
            {
                return this.reportItemIDField;
            }
            set
            {
                this.reportItemIDField = value;
            }
        }

        /// <remarks/>
        public string TuneElementID
        {
            get
            {
                return this.tuneElementIDField;
            }
            set
            {
                this.tuneElementIDField = value;
            }
        }

        /// <remarks/>
        public string MZ
        {
            get
            {
                return this.mzField;
            }
            set
            {
                this.mzField = value;
            }
        }

        /// <remarks/>
        public string Height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }

        /// <remarks/>
        public string Axis
        {
            get
            {
                return this.axisField;
            }
            set
            {
                this.axisField = value;
            }
        }

        /// <remarks/>
        public string Width10
        {
            get
            {
                return this.width10Field;
            }
            set
            {
                this.width10Field = value;
            }
        }

        /// <remarks/>
        public string Width10PosX
        {
            get
            {
                return this.width10PosXField;
            }
            set
            {
                this.width10PosXField = value;
            }
        }

        /// <remarks/>
        public string Width10PosY
        {
            get
            {
                return this.width10PosYField;
            }
            set
            {
                this.width10PosYField = value;
            }
        }

        /// <remarks/>
        public string Width50
        {
            get
            {
                return this.width50Field;
            }
            set
            {
                this.width50Field = value;
            }
        }

        /// <remarks/>
        public string Width50PosX
        {
            get
            {
                return this.width50PosXField;
            }
            set
            {
                this.width50PosXField = value;
            }
        }

        /// <remarks/>
        public string Width50PosY
        {
            get
            {
                return this.width50PosYField;
            }
            set
            {
                this.width50PosYField = value;
            }
        }

        /// <remarks/>
        public string PeakHeightPercent
        {
            get
            {
                return this.peakHeightPercentField;
            }
            set
            {
                this.peakHeightPercentField = value;
            }
        }

        /// <remarks/>
        public string Width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        /// <remarks/>
        public string WidthPosX
        {
            get
            {
                return this.widthPosXField;
            }
            set
            {
                this.widthPosXField = value;
            }
        }

        /// <remarks/>
        public string WidthPosY
        {
            get
            {
                return this.widthPosYField;
            }
            set
            {
                this.widthPosYField = value;
            }
        }

        /// <remarks/>
        public string Count
        {
            get
            {
                return this.countField;
            }
            set
            {
                this.countField = value;
            }
        }

        /// <remarks/>
        public string Rsd
        {
            get
            {
                return this.rsdField;
            }
            set
            {
                this.rsdField = value;
            }
        }

        /// <remarks/>
        public string ResponseRatio
        {
            get
            {
                return this.responseRatioField;
            }
            set
            {
                this.responseRatioField = value;
            }
        }

        /// <remarks/>
        public string Cps
        {
            get
            {
                return this.cpsField;
            }
            set
            {
                this.cpsField = value;
            }
        }

        /// <remarks/>
        public string SampleConc
        {
            get
            {
                return this.sampleConcField;
            }
            set
            {
                this.sampleConcField = value;
            }
        }

        /// <remarks/>
        public string IsReference
        {
            get
            {
                return this.isReferenceField;
            }
            set
            {
                this.isReferenceField = value;
            }
        }

        /// <remarks/>
        public string PlotRangeYAxis
        {
            get
            {
                return this.plotRangeYAxisField;
            }
            set
            {
                this.plotRangeYAxisField = value;
            }
        }

        /// <remarks/>
        public string ConvertedXValues
        {
            get
            {
                return this.convertedXValuesField;
            }
            set
            {
                this.convertedXValuesField = value;
            }
        }

        /// <remarks/>
        public string ConvertedYValues
        {
            get
            {
                return this.convertedYValuesField;
            }
            set
            {
                this.convertedYValuesField = value;
            }
        }

        /// <remarks/>
        public string AxisCheckPass
        {
            get
            {
                return this.axisCheckPassField;
            }
            set
            {
                this.axisCheckPassField = value;
            }
        }

        /// <remarks/>
        public string AxisCheckMin
        {
            get
            {
                return this.axisCheckMinField;
            }
            set
            {
                this.axisCheckMinField = value;
            }
        }

        /// <remarks/>
        public string AxisCheckMax
        {
            get
            {
                return this.axisCheckMaxField;
            }
            set
            {
                this.axisCheckMaxField = value;
            }
        }

        /// <remarks/>
        public string WidthCheckPass
        {
            get
            {
                return this.widthCheckPassField;
            }
            set
            {
                this.widthCheckPassField = value;
            }
        }

        /// <remarks/>
        public string WidthCheckMax
        {
            get
            {
                return this.widthCheckMaxField;
            }
            set
            {
                this.widthCheckMaxField = value;
            }
        }

        /// <remarks/>
        public string ResponseCheckPass
        {
            get
            {
                return this.responseCheckPassField;
            }
            set
            {
                this.responseCheckPassField = value;
            }
        }

        /// <remarks/>
        public string ResponseCheckMin
        {
            get
            {
                return this.responseCheckMinField;
            }
            set
            {
                this.responseCheckMinField = value;
            }
        }

        /// <remarks/>
        public string RsdCheckPass
        {
            get
            {
                return this.rsdCheckPassField;
            }
            set
            {
                this.rsdCheckPassField = value;
            }
        }

        /// <remarks/>
        public string RsdCheckMax
        {
            get
            {
                return this.rsdCheckMaxField;
            }
            set
            {
                this.rsdCheckMaxField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "TuneReport")]
    public partial class TuneReportDataSetTuneParameter
    {

        private string tuneReportIDField;

        private string reportSetIDField;

        private string tuneParamIDField;

        private string nameField;

        private string valueField;

        /// <remarks/>
        public string TuneReportID
        {
            get
            {
                return this.tuneReportIDField;
            }
            set
            {
                this.tuneReportIDField = value;
            }
        }

        /// <remarks/>
        public string ReportSetID
        {
            get
            {
                return this.reportSetIDField;
            }
            set
            {
                this.reportSetIDField = value;
            }
        }

        /// <remarks/>
        public string TuneParamID
        {
            get
            {
                return this.tuneParamIDField;
            }
            set
            {
                this.tuneParamIDField = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "TuneReport")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "TuneReport", IsNullable = false)]
    public partial class NewDataSet
    {

        private TuneReportDataSet[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TuneReportDataSet")]
        public TuneReportDataSet[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }
}