﻿using System;
using System.Text;
using JetBrains.Annotations;

namespace LibNbt {
    /// <summary> A tag containing a single signed 32-bit integer. </summary>
    public sealed class NbtInt : NbtTag, INbtTagValue<int> {
        /// <summary> Type of this tag (Int). </summary>
        public override NbtTagType TagType {
            get { return NbtTagType.Int; }
        }

        /// <summary> Value/payload of this tag (a single signed 32-bit integer). </summary>
        public int Value { get; set; }


        /// <summary> Creates an unnamed NbtInt tag with the default value of 0. </summary>
        public NbtInt() {}


        /// <summary> Creates an unnamed NbtInt tag with the given value. </summary>
        /// <param name="value"> Value to assign to this tag. </param>
        public NbtInt( int value )
            : this( null, value ) {}


        /// <summary> Creates an NbtInt tag with the given name and the default value of 0. </summary>
        /// <param name="tagName"> Name to assign to this tag. May be null. </param>
        public NbtInt( [CanBeNull] string tagName )
            : this( tagName, 0 ) {}


        /// <summary> Creates an NbtInt tag with the given name and value. </summary>
        /// <param name="tagName"> Name to assign to this tag. May be null. </param>
        /// <param name="value"> Value to assign to this tag. </param>
        public NbtInt( [CanBeNull] string tagName, int value ) {
            Name = tagName;
            Value = value;
        }


        internal void ReadTag( NbtReader readStream, bool readName ) {
            if( readName ) {
                Name = readStream.ReadString();
            }
            Value = readStream.ReadInt32();
        }


        internal override void WriteTag( NbtWriter writeStream, bool writeName ) {
            writeStream.Write( NbtTagType.Int );
            if( writeName ) {
                if( Name == null ) throw new NbtFormatException( "Name is null" );
                writeStream.Write( Name );
            }
            writeStream.Write( Value );
        }


        internal override void WriteData( NbtWriter writeStream ) {
            writeStream.Write( Value );
        }


        /// <summary> Returns a String that represents the current NbtInt object.
        /// Format: TAG_Int("Name"): Value </summary>
        /// <returns> A String that represents the current NbtInt object. </returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append( "TAG_Int" );
            if( !String.IsNullOrEmpty( Name ) ) {
                sb.AppendFormat( "(\"{0}\")", Name );
            }
            sb.AppendFormat( ": {0}", Value );
            return sb.ToString();
        }
    }
}