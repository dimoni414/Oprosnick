using Mvvm.Core.Abstract;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab19_Oprosnik.Services

{
    public class BinarySerializationService : ISerializationService
    {
        public T Deserialize<T>(Stream stream) =>
            (T)_formatter.Deserialize(stream);

        public void Serialize<T>(Stream stream, T data) =>
            _formatter.Serialize(stream, data);

        private readonly BinaryFormatter _formatter = new BinaryFormatter();
    }
}