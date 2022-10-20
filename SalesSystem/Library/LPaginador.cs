using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Library
{
    public class LPaginador<T>
    {
        private int pagi_cuantos = 8;
        private int pagi_nav_num_enlaces = 3;
        private int pagi_actual;
        private String pagi_nav_anterior = "&laquo;Anterior";
        private String pagi_nav_siguiente = "Siguiente;&raqueo";
        private String pagi_nav_primera = "&laquo; Primero";
        private String pagi_nav_ultima = "ultimo;&raquo";
        private String pagi_navegacion = null;

        public object[]paginador(List<T>Table,int pagina, int registros,String area,String controller,String action,String host
            )
        {
            pagi_actual = pagina == 0 ? 1 : pagina;
            pagi_cuantos = registros > 0 ? registros  : pagi_cuantos;
            int pagi_totalReg = Table.Count;
            double valor1 = Math.Ceiling((double)pagi_totalReg / (double)pagi_cuantos);
            int pagi_totalPags = Convert.ToInt16(Math.Ceiling(valor1));
            if(pagi_actual!=1)
            {
                int pagi_url = 1;
                pagi_navegacion += "a class='btn btn-default' href='" + host + "/" + controller + "/" + action + "?id=" + pagi_url + "&area=" + area + "'>" + pagi_nav_primera + "</a";


                pagi_url = pagi_actual - 1;
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" + action + "?id=" + pagi_url + "&registros=" + pagi_cuantos + "&area=" + area + "'>" + area + "'>" + pagi_nav_anterior + "</a>";
                    
            }
            double valor2 = (pagi_nav_num_enlaces / 2);
            int pagi_nav_intervalo = Convert.ToInt16(Math.Round(valor2));
            int pagi_nav_desde = pagi_actual - pagi_nav_intervalo;
            int pagi_nav_hasta = pagi_actual + pagi_nav_intervalo;
            if(pagi_nav_desde < 1)
            {
                pagi_nav_hasta -= (pagi_nav_desde - 1);
                pagi_nav_desde = 1;
            }
            if(pagi_nav_hasta > pagi_totalPags )
            {
                pagi_nav_desde -= (pagi_nav_hasta - pagi_totalPags );
                pagi_nav_hasta = pagi_totalPags;

                if(pagi_nav_desde < 1)
                {
                    pagi_nav_desde = 1;
                }
            }
            for(int pagi_i=pagi_nav_desde; pagi_i <= pagi_nav_hasta; pagi_i++)
            {
                if(pagi_i == pagi_actual)
                {
                    pagi_navegacion += "<span class ='btn btn-default' disabled='disabled'>" + pagi_i + "</span";

                }
                else
                {
                    pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" + action + "?id=" + pagi_i + "&registros=" + pagi_cuantos + "&area" + area + "'>" + pagi_i + "</a>";
                }
            }
            if(pagi_actual<pagi_totalPags)
            {
                int pagi_url = pagi_actual + 1;
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" + action + "?id=" + pagi_url + "&registros=" + pagi_cuantos + "&area" + area + "'>" + pagi_nav_siguiente + "</a>";

                pagi_url = pagi_totalPags;
                pagi_navegacion += "<a class ='btn btn-default' href'" + host + "/" + controller + "/" + action + "?id=" + pagi_url + "&registros=" + pagi_cuantos + "&area" + area + "'>" + pagi_nav_ultima + "</a>";

            }
            int pagi_inicial = (pagi_actual - 1) * pagi_cuantos;
            var query = Table.Skip(pagi_inicial).Take(pagi_cuantos).ToList();//podria fallar
            String pagi_info = "del <b>" + pagi_actual + "</b> al <b>" + pagi_totalPags + "</b> de <b>" + pagi_totalReg + "</b><b>/" + pagi_cuantos + "</b>";
            object[] data = { pagi_info, pagi_navegacion, query };
            return data;
        }



    }
}
