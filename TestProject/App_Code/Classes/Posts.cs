﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Posts
/// </summary>
public class Posts
{
    public Posts()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Posts(string username,string _post, int _postType)
    {
        user = UserManager.getUser(username,"Username");
        post = _post;
        postingDate = DateTime.Now;
        postType = _postType;
    }
    public Posts(string _pId,string _post, string _postdate,string username)
    {
        user = UserManager.getUser(username, "Username");
        post = _post;
        pId = _pId;
        postingDate = Convert.ToDateTime(_postdate);
    }
    public string pId { get; private set; }
    public User user { get; private set; }
    public string post { get; set; }
    public DateTime postingDate { get; private set; }
    public int postType { get; private set; }
}