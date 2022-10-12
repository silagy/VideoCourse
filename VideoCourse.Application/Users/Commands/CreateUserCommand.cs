﻿using MediatR;
using VideoCourse.Application.Users.Common;
using ErrorOr;
using VideoCourse.Domain.Enums;

namespace VideoCourse.Application.Users.Commands;

public record CreateUserCommand(string FirstName, string LastName, string Email, string Password, UserRole Role) : IRequest<ErrorOr<UserResponse>>;