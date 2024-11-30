using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hostel_Management.Models;
using Hostel_Management.Services;
using System;

namespace Hostel_Management.Controllers
{
    [ApiController]
    [Route("api/room")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [Authorize(Roles = "Manager, Receptionist")]
        [HttpGet]
        public IActionResult GetAllRooms()
        {
            var rooms = _roomService.GetAllRooms();
            return Ok(rooms);
        }

        [Authorize(Roles = "Manager, Receptionist")]
        [HttpGet("{roomId}")]
        public IActionResult GetRoomById(int roomId)
        {
            var room = _roomService.GetRoomById(roomId);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        [Authorize(Roles = "Manager, Receptionist")]
        [HttpPost]
        public IActionResult AddRoom([FromBody] Room room)
        {
            _roomService.AddRoom(room);
            return CreatedAtAction(nameof(GetRoomById), new { roomId = room.RoomId }, room);
        }

        [Authorize(Roles = "Manager, Receptionist")]
        [HttpPut("{roomId}")]
        public IActionResult UpdateRoom(int roomId, [FromBody] Room room)
        {
            if (roomId != room.RoomId)
            {
                return BadRequest();
            }

            _roomService.UpdateRoom(room);
            return Ok("Room Updated");
        }

        [Authorize(Roles = "Manager, Receptionist")]
        [HttpDelete("{roomId}")]
        public IActionResult DeleteRoom(int roomId)
        {
            _roomService.DeleteRoom(roomId);
            return Ok("Room Deleted");
        }

        
    }
}

