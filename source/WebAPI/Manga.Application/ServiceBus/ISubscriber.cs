﻿namespace Manga.Application.ServiceBus
{
    using MediatR;

    public interface ISubscriber
    {
        void Listen(IMediator mediator);
    }
}
