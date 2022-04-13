using Microsoft.AspNetCore.Mvc;
using MultiplayerPokemon.Server.Attributes;
using MultiplayerPokemon.Server.Orchestrators.Interfaces;
using MultiplayerPokemon.Shared.Dtos;

namespace MultiplayerPokemon.Server.Controllers
{
    [Authorize]
    public class RoomController : ControllerBase
    {
        private readonly IRoomOrchestrator roomOrchestrator;

        public RoomController(IRoomOrchestrator _roomOrchestrator)
        {
            roomOrchestrator = _roomOrchestrator;
        }

        [HttpGet("GetRoomListData")]
        public async Task<IEnumerable<RoomData>> GetRoomListData()
        {
            return await roomOrchestrator.GetRoomListData();
        }

        [HttpPost("CreateRoom")]
        public async Task<CreateRoomResult> CreateRoom([FromBody] CreateRoomRequest request)
        {
            return await roomOrchestrator.CreateRoom(request);
        }
    }
}
