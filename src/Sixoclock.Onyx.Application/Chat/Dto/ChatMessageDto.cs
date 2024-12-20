using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Sixoclock.Onyx.Chat.Dto
{
    [AutoMapFrom(typeof(ChatMessage))]
    public class ChatMessageDto : EntityDto
    {
        public long UserId { get; set; }

        public int? TenantId { get; set; }

        public long TargetUserId { get; set; }

        public int? TargetTenantId { get; set; }

        public ChatSide Side { get; set; }

        public ChatMessageReadState ReadState { get; set; }

        public ChatMessageReadState ReceiverReadState { get; set; }

        public string Message { get; set; }
        
        public DateTime CreationTime { get; set; }

        public string SharedMessageId { get; set; }
    }
}