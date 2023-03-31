using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALL.Models;
using DALL.DTOs;

namespace DALL.DTOs
{
    public class MapperConfig
    {
        public static MapperConfiguration mapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<grupo_familiar, grupo_familiarDTO>(); //Get
                cfg.CreateMap<grupo_familiarDTO, grupo_familiar>(); //Post - Put

                cfg.CreateMap<usuarios, usuariosDTO>(); //Get
                cfg.CreateMap<usuariosDTO, usuarios>(); //Post - Put
            });
        }
    }
}
