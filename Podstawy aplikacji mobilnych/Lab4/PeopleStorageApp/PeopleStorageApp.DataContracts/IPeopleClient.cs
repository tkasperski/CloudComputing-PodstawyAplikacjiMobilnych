using RestEase;
using System.Threading.Tasks;

namespace PeopleStorageApp.DataContracts
{
    public interface IPeopleClient
    {
        [Post("people")]
        Task AddPersonAsync([Body] Person person);
    }
}