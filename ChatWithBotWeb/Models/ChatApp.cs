﻿//using ChatWithBotWeb.Models.Interface;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ChatWithBotWeb.Models
//{
//    class ChatApp
//    {
//        //public List<Chat> ListChats = new List<Chat>();
//        //public List<IBot> Bots;
//        //private List<User> Users = new List<User>();

//        //IContext Context;

//        //public ChatApp(IContext context)
//        //{
//        //    Context = context;
//        //    ListChats = context.GetAllChats();
//        //    Users = context.GetUsers();
//        //    Bots = context.GetAllBot();
//        //}

//        //public User GetUser(string userName)
//        //{
//        //    User user = Users.Find(u => u.Name.Equals(userName));
//        //    if (user == null)
//        //    {
//        //        user = new User(userName);
//        //        Users.Add(user);
//        //        Context.CreatUser(Users);
//        //        return user;
//        //    }
//        //    else
//        //    {
//        //        return user;
//        //    }
//        //}
//        //public void CreateChat(Chat chat)
//        //{
//        //    ListChats.Add(chat);
//        //    Context.CreatChat(ListChats);
//        //}
//        //public void DeleteChat(Chat chat, User user)
//        //{
//        //    if (chat.Users.Contains(user))
//        //    {
//        //        chat.ChatLogUsers[user.Name].StopChat = DateTime.Now;
//        //        chat.Users.Remove(user);
//        //        ListChats.Remove(chat);
//        //        Context.CreatChat(ListChats);
//        //    }
//        //    else
//        //    {
//        //        throw new Exception("Вы не можете удалить чат, не являясь его участником");
//        //    }
//        //}
//        public string GetHistoryChat(Chat chat, User user)
//        {
//            string result = "";
//            try
//            {
//                if (chat.ChatLogUsers[user.Name] != null)
//                {
//                    var mes = chat.ListMessage.Where(m => chat.ChatLogUsers[user.Name].StartChat <= m.dateTime);
//                    if (chat.ChatLogUsers[user.Name].StopChat != null)
//                    {
//                        mes = chat.ListMessage.Where(m => m.dateTime < chat.ChatLogUsers[user.Name].StopChat);
//                    }
//                    if (mes.Any())
//                    {
//                        foreach (var m in mes)
//                        {
//                            result += ($"id={m.MessageId + 1} {m.dateTime} {m.User.Name} : {m.Content} \n");
//                        }
//                        return result;
//                    }
//                }
//            }
//            catch
//            {
//                result = "Сообщений пока нет";
//            }
//            return result;
//        }
//        //public void InviteUser(Chat chat, User user, string nameInvite)
//        //{
//        //    var Inviteuser = Context.GetUsers().Where(u => u.Name == nameInvite).FirstOrDefault();
//        //    if (Inviteuser == null)
//        //    {
//        //        throw new ArgumentNullException("Пользователь не существет или уже в чате");
//        //    }
//        //    user.SignUser(chat, Inviteuser);
//        //    Context.CreatChat(ListChats);
//        //}
//        //public void DeleteUser(Chat chat, User user, string nameDelete)
//        //{
//        //    if (chat.Users.Contains(user))
//        //    {
//        //        user.LogutUser(chat, user, nameDelete);
//        //        Context.CreatChat(ListChats);
//        //    }
//        //    else
//        //    {
//        //        throw new Exception("Вы не являись участником \n Чтобы присоединиться используйте команду sign @usernames");
//        //    }
//        //}
//        //public Message SendMessege (Chat chat, User user,string contetn,string nameSend)
//        //{
//        //    if (chat.Users.Contains(user))
//        //    {
//        //        var message = new Message(contetn, nameSend, chat, user);
//        //        chat.ListMessage.Add(message);
//        //        Context.CreatChat(ListChats);
//        //        return message;
//        //    }
//        //    else
//        //    {
//        //        throw new Exception("Вы не являись участником \n Чтобы присоединиться используйте команду sign @usernames");
//        //    }
//        //}
//        //public void DeleteMessge(Chat chat, int index, User user)
//        //{
//        //    user.DelMessage(chat, index, user);
//        //}
//        //public Message BotMove(Chat chat, User user, string botName,string command)
//        //{
//        //    if (chat.Users.Contains(user))
//        //    {
//        //        if (chat.ChatBot.Where(b => b.NameBot == botName).Any())
//        //        {
//        //            string Contetn = "";
//        //            foreach (var b in chat.ChatBot)
//        //            {
//        //                if (b.NameBot == botName)
//        //                {
//        //                    Contetn = b.Move(command);
//        //                }
//        //            }
//        //            if (!String.IsNullOrEmpty(Contetn))
//        //            {
//        //                var message = new Message(Contetn, botName, chat, user); ///Есть вывод в консоль на условие
//        //                chat.ListMessage.Add(message);
//        //                Context.CreatChat(ListChats);
//        //                return message;
//        //            }
//        //            else
//        //            {
//        //                throw new ArgumentException("Некорректная команда");
//        //            }
//        //        }
//        //        else
//        //        {
//        //            throw new ArgumentException("Такого бота в чатe нет ((");
//        //        }
//        //    }
//        //    else
//        //    {
//        //        throw new Exception("Вы не являись участником \n Чтобы присоединиться используйте команду sign @usernames");
//        //    }
//        //}
//        //public IBot InviteBot(int indexBot,Chat chat, User user)
//        //{
//        //    if (chat.Users.Contains(user))
//        //    {
//        //        IBot bot = Bots[indexBot];
//        //        chat.ChatBot.Add(bot);
//        //        Context.CreatChat(ListChats);
//        //        return bot;
//        //    }else
//        //    {
//        //        throw new Exception("Вы не являись участником \n Чтобы присоединиться используйте команду sign @usernames");
//        //    }

//        //}
//    }
//}
