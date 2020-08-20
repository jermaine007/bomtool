using Noone.UI.Models;
using System;

namespace BomTool.NetCore.Models
{
    /// <summary>
    /// Infromation data model from excel
    /// </summary>
    public class Information : ModelBase
    {
        /// <summary>
        /// Empty data
        /// </summary>
        internal static readonly Information Empty = new Information
        {
            Picture = string.Empty,
            Approver = string.Empty,
            ApproveDate = string.Empty,
            PreparedBy = string.Empty,
            PreparedDate = string.Empty,
            DrawingNo = string.Empty,
            Version = string.Empty
        };

        /// <summary>
        /// Picture path
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// Approver
        /// </summary>
        public string Approver { get; set; }

        /// <summary>
        /// ApproveDate
        /// </summary>
        public string ApproveDate { get; set; }

        /// <summary>
        /// PreparedBy
        /// </summary>
        public string PreparedBy { get; set; }

        /// <summary>
        /// PreparedDate
        /// </summary>
        public string PreparedDate { get; set; }

        /// <summary>
        /// DrawingNo
        /// </summary>
        public string DrawingNo { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        public string Version { get; set; }


        public override string ToString() => string.Join(Environment.NewLine, new[] {
            $"Picture -> {Picture}",
            $"Approver -> {Approver}",
            $"ApproveDate -> {ApproveDate}",
            $"PreparedBy -> {PreparedBy}",
            $"PreparedDate -> {PreparedDate}",
            $"DrawingNo -> {DrawingNo}",
            $"Version -> {Version}"

        });

    }
}
