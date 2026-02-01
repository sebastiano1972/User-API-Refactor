// Global using directives

global using System;
global using System.Collections.Concurrent;
global using System.ComponentModel.DataAnnotations;
global using System.Linq.Expressions;
global using MediatR;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Tests.User.Application.Abstractions;
global using Tests.User.Application.DTOs.Book;
global using Tests.User.Application.DTOs.Borrowings;
global using Tests.User.Application.DTOs.Comment;
global using Tests.User.Application.DTOs.User;
global using Tests.User.Application.Enums;
global using Tests.User.Application.EventSourcing;
global using Tests.User.Application.Exceptions;
global using Tests.User.Application.Extensions;
global using Tests.User.Application.Features.Books.Commands.UpdateBook;
global using Tests.User.Application.Mappers;
global using Tests.User.Application.Specifications;
global using Tests.User.Application.State;
global using Tests.User.Domain.Abstractions;
global using Tests.User.Domain.Entities;
global using Tests.User.Domain.Events;