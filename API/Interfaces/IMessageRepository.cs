using API.DTOs;
using API.Entity;
using API.Helpers;

namespace API.Interfaces
{
 public interface IMessageRepository
 {
  void AddMessage(Message message);
  void DeleteMessage(Message message);
  Task<Message> GetMessage(int id);
  Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams);
  Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName, string recipientUserName);
  Task<bool> SaveAllAsync();
  //! method for tracking group
  void AddGroup(Group group);
  void RemoveConnection(Connection connection);
  Task<Connection> GetConnection(string connectionId);
  Task<Group> GetMessageGroup(string groupName);

  // ! get the group basedinconnection id
  Task<Group> GetGroupForConnection(string connectionId);
 }
}