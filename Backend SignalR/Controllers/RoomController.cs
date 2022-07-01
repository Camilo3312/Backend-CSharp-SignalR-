using APIMySQL.Data.Repositories;
using APIMySQL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_SignalR.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpPost]
        [Route("room/new/{iduser1}/{iduser2}/{idservice}")]
        public async Task<IActionResult> CreateRoom(int iduser1, int iduser2, int idservice)
        {
            var new_room = new Room();
            new_room.fechainicio = "2022-04-16";
            new_room.fechafin = "2022-04-16";
            new_room.horainicio = "13:00";
            new_room.horafin = "13:00";
            new_room.servicio = idservice;

            return Ok(await _roomRepository.CreateRoom(new_room, iduser1, iduser2));
        }

        [HttpGet]
        [Route("chats/{token}")]
        public async Task<IActionResult> GetRooms(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedValue = handler.ReadJwtToken(token);
            var id = decodedValue.Payload.GetValueOrDefault("userId");

            return Ok(await _roomRepository.GetRoomUser(Convert.ToInt32(id)));
        }

        [HttpGet]
        [Route("room/delete/{idroom}")]
        public async Task<IActionResult> DeleteRoom(int idroom)
        {
            return Ok(await _roomRepository.DeleteRoom(Convert.ToInt32(idroom)));
        }

        [HttpGet]
        [Route("messages/{room}")]
        public async Task<IActionResult> GetMessagesRoom(int room)
        {
            return Ok(await _roomRepository.GetMessagesUser(Convert.ToInt32(room)));
        }

        [HttpPost]
        [Route("message/new")]
        public async Task<IActionResult> CreateMessage([FromBody] Message message)
        {
            return Ok(await _roomRepository.CreateMessage(message));
        }

    }
}
