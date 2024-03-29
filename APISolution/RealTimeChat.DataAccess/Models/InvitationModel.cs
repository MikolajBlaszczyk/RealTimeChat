﻿namespace RealTimeChat.DataAccess.Models;

public class InvitationModel
{
    public string SenderId { get; set; }
    public string ResponderId { get; set; }
    
    public ApplicationUser Sender { get; set; }
    public ApplicationUser Responder { get; set; }
    
    public string Status { get; set; } = string.Empty;
}