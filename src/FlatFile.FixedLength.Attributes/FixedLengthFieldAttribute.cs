using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using FlatFile.Core.Attributes.Extensions;

namespace FlatFile.FixedLength.Attributes
{
    using FlatFile.Core.Attributes.Base;

    public class FixedLengthFieldAttribute : FieldSettingsBaseAttribute
    {
        public int Lenght { get; protected set; }
        
        public Padding Padding { get; set; }

        public bool PadLeft
        {
            get { return Padding == Padding.Left; }
        }

        public char PaddingChar { get; set; }

        public FixedLengthFieldAttribute(int index, int lenght)
            : base(index)
        {
            Padding = Padding.Left;

            Lenght = lenght;
        }
    }
}