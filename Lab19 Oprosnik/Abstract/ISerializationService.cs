using System.IO;

namespace Mvvm.Core.Abstract
{
	public interface ISerializationService
	{
		T Deserialize<T>(Stream stream);
		void Serialize<T>(Stream stream, T data);
	}
}
