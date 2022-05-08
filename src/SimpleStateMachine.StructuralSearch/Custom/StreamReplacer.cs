using System.IO;
using System.Threading.Tasks;

namespace SimpleStateMachine.StructuralSearch.Custom;

// public class StreamReplacer : Stream
// {
//     
//     private readonly Stream _baseStream;
//     
//     public StreamReplacer(Stream baseStream)
//     {
//         _baseStream = baseStream;
//     }
//     
//     public Task Replace(Stream stream, int fromStart, int fromEnd, int toStart, int toEnd)
//     {
//         stream.SetLength();
//         stream.Position = fromStart;
//         stream.
//     }
//     
//     public override void Flush()
//     {
//         throw new System.NotImplementedException();
//     }
//
//     public override int Read(byte[] buffer, int offset, int count)
//     {
//         throw new System.NotImplementedException();
//     }
//
//     public override long Seek(long offset, SeekOrigin origin)
//     {
//         throw new System.NotImplementedException();
//     }
//
//     public override void SetLength(long value)
//     {
//         throw new System.NotImplementedException();
//     }
//
//     public override void Write(byte[] buffer, int offset, int count)
//     {
//         throw new System.NotImplementedException();
//     }
//
//     public override bool CanRead { get; }
//     public override bool CanSeek { get; }
//     public override bool CanWrite { get; }
//     public override long Length { get; }
//     public override long Position { get; set; }
// }