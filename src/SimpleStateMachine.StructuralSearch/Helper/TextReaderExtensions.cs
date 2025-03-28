﻿using System;
using System.IO;

namespace SimpleStateMachine.StructuralSearch.Helper;

internal static class TextReaderExtensions
{
    /// <summary>
    /// Copies a specific number of bytes from the current position of the
    /// source stream to the current position of the destination stream.
    /// </summary>
    /// <param name="source">The source stream.</param>
    /// <param name="destination">The destination stream.</param>
    /// <param name="count">The number of bytes to copy.</param>
    /// <param name="bufferSize">The size of the buffer to use.
    /// The default is 4096.</param>
    public static void CopyPartTo(this TextReader source, TextWriter destination, int offset, int count, int bufferSize)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (destination == null) throw new ArgumentNullException(nameof(destination));

        char[] buffer = new char[bufferSize];
        int read;
        
        while (count > 0 && (read = source.ReadBlock(buffer)) > 0)
        {
            count -= read;
            destination.Write(buffer, offset, read);
        }
    }

    /// <summary>
    /// Copies a specific number of bytes from the current position of the
    /// source stream to the current position of the destination stream.
    /// </summary>
    /// <param name="source">The source stream.</param>
    /// <param name="destination">The destination stream.</param>
    /// <param name="count">The number of bytes to copy.</param>
    public static void CopyPartTo(this TextReader source, TextWriter destination, int offset, int count)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (destination == null) throw new ArgumentNullException(nameof(destination));
            
        CopyPartTo(source, destination, offset, count, 4096);
    }
}