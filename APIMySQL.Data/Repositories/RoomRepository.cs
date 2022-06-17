using APIMySQL.Model;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIMySQL.Data.Repositories
{
    public class RoomRepository : IRoomRepository 
    {
        private MySQLConfiguration _connectionString;
        public RoomRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString._connectionString);
        }

        public async Task<bool> CreateRoom(Room room, int iduser1, int iduser2)
        {
            using (var database = dbConnection()) {
                var query = @"INSERT INTO sala (fechainicio, fechafin, horainicio, horafin, servicio)
                              VALUES (@fechainicio, @fechafin, @horainicio, @horafin, @servicio);
                             ";
                    query += $"INSERT INTO sala_usuario (idsala, idusuario) VALUES (@@identity, {iduser1} ); INSERT INTO sala_usuario(idsala, idusuario) VALUES((select max(idsala) from sala), {iduser2} );";
                var response = database.ExecuteAsync(query, room);
                return await response > 0;
            }
        }

        public async Task<IEnumerable> GetRoomUser(int id)
        {
            using (var database = dbConnection()) {
                var query = @"
                     select u.apellidos, u.nombres, u.fotop, u.color, u.idusuario, su.idsala, s.nombre as nombreservicio, s.foto, s.idservicio 
                     from sala_usuario su inner join usuarios u on su.idusuario = u.idusuario 
                     left join sala sl on su.idsala = sl.idsala
                     inner join servicios s on  sl.servicio = s.idservicio
                     where su.idsala in(select idsala from sala_usuario where idusuario =  @id) and u.idusuario != @id;
                ";
                return await database.QueryAsync(query, new { id = id });
            }
        }

        public async Task<IEnumerable> GetMessagesUser(int id)
        {
            using(var database = dbConnection()) {
                var query = @"
                        SELECT m.idmensaje, u.nombres AS usuario, m.mensaje AS messagess, m.fecha 
                        FROM mensajes m INNER JOIN usuarios u on m.idusuario = u.idusuario
                        WHERE m.idsala = @id
                        ORDER BY m.idmensaje;
                ";
                return await database.QueryAsync(query, new { id = id });
            }           
        }

        public async Task<bool> CreateMessage(Message message)
        {
            using (var database = dbConnection()) {

                var handler = new JwtSecurityTokenHandler();
                var decodedValue = handler.ReadJwtToken(message.token);
                var id = decodedValue.Payload.GetValueOrDefault("userId");
                message.idusuario = Convert.ToInt32(id);

                var query = @"
                        INSERT INTO mensajes (mensaje, idsala, idusuario)
                        VALUES (@mensaje, @idsala, @idusuario)
                ";
                var response = database.ExecuteAsync(query, message);
                return await response > 0;
            }
        }
    }
}
