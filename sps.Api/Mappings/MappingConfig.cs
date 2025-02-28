﻿using sps.Domain.Model.Entities;
using sps.Domain.Model.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;

/// <summary>
/// Mapping configuration class.
/// </summary>
public static class MappingConfig
{
    /// <summary>
    /// Register mappings.
    /// </summary>
    public static void RegisterMappings()
    {
        // Identity User mapping
        TypeAdapterConfig<IdentityUser<Guid>, AppUserModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.UserName, src => src.UserName)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber);

        // UserProfile mapping
        TypeAdapterConfig<UserProfile, UserProfileModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.ApartmentNumber, src => src.ApartmentNumber)
            .Map(dest => dest.PhoneNumberSecondary, src => src.PhoneNumberSecondary)
            .Map(dest => dest.EmailOptOut, src => src.EmailOptOut)
            .Map(dest => dest.SmsOptOut, src => src.SmsOptOut)
            .Map(dest => dest.PinCode, src => src.PinCode);

        TypeAdapterConfig<UserProfileModel, UserProfile>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.ApartmentNumber, src => src.ApartmentNumber)
            .Map(dest => dest.PhoneNumberSecondary, src => src.PhoneNumberSecondary)
            .Map(dest => dest.EmailOptOut, src => src.EmailOptOut)
            .Map(dest => dest.SmsOptOut, src => src.SmsOptOut)
            .Map(dest => dest.PinCode, src => src.PinCode);

        // Booking mapping
        TypeAdapterConfig<Booking, BookingModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.TimeslotId, src => src.TimeslotId)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.UpdatedAt, src => src.UpdatedAt);

        TypeAdapterConfig<BookingModel, Booking>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.TimeslotId, src => src.TimeslotId)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.UpdatedAt, src => src.UpdatedAt);

        // DesiredTimeslot mapping
        TypeAdapterConfig<DesiredTimeslot, DesiredTimeslotModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.TimeslotId, src => src.TimeslotId)
            .Map(dest => dest.NotificationSent, src => src.NotificationSent)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt);

        TypeAdapterConfig<DesiredTimeslotModel, DesiredTimeslot>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.TimeslotId, src => src.TimeslotId)
            .Map(dest => dest.NotificationSent, src => src.NotificationSent)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt);

        // Room mapping
        TypeAdapterConfig<Room, RoomModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Location, src => src.Location)
            .Map(dest => dest.IsAvailable, src => src.IsAvailable)
            .Map(dest => dest.MaxCapacity, src => src.MaxCapacity);

        TypeAdapterConfig<RoomModel, Room>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Location, src => src.Location)
            .Map(dest => dest.IsAvailable, src => src.IsAvailable)
            .Map(dest => dest.MaxCapacity, src => src.MaxCapacity);

        // Timeslot mapping
        TypeAdapterConfig<Timeslot, TimeslotModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.RoomId, src => src.RoomId)
            .Map(dest => dest.StartTime, src => src.SlotTime.Start)
            .Map(dest => dest.EndTime, src => src.SlotTime.End)
            .Map(dest => dest.IsAvailable, src => src.IsAvailable)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.UpdatedAt, src => src.UpdatedAt);

        TypeAdapterConfig<TimeslotModel, Timeslot>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.RoomId, src => src.RoomId)
            .Map(dest => dest.SlotTime.Start, src => src.StartTime)
            .Map(dest => dest.SlotTime.End, src => src.EndTime)
            .Map(dest => dest.IsAvailable, src => src.IsAvailable)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.UpdatedAt, src => src.UpdatedAt);

        // LostAndFound mapping
        TypeAdapterConfig<LostAndFound, LostAndFoundModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.PictureUrl, src => src.PictureUrl)
            .Map(dest => dest.DateFound, src => src.DateFound);

        TypeAdapterConfig<LostAndFoundModel, LostAndFound>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.PictureUrl, src => src.PictureUrl)
            .Map(dest => dest.DateFound, src => src.DateFound);

        // ServiceMessage mapping
        TypeAdapterConfig<ServiceMessage, ServiceMessageModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Message, src => src.Message)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.IsRead, src => src.IsRead);

        TypeAdapterConfig<ServiceMessageModel, ServiceMessage>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Message, src => src.Message)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.IsRead, src => src.IsRead);

        // ChatMessage mapping
        TypeAdapterConfig<ChatMessage, ChatMessageModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.SenderId, src => src.SenderId)
            .Map(dest => dest.ReceiverId, src => src.ReceiverId)
            .Map(dest => dest.Message, src => src.Message)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.IsRead, src => src.IsRead);

        TypeAdapterConfig<ChatMessageModel, ChatMessage>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.SenderId, src => src.SenderId)
            .Map(dest => dest.ReceiverId, src => src.ReceiverId)
            .Map(dest => dest.Message, src => src.Message)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.IsRead, src => src.IsRead);
    }
}