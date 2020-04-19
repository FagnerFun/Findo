using FindoApp.Domain.Interface.Service;
using FindoApp.Domain.Model;
using FindoApp.Domain.Model.Enum;
using System;
using System.Collections.Generic;

namespace FindoApp.Service
{
    public class CheckListService : ICheckListService
    {
        public List<CheckList> GetCheckLists()
        {
            CheckList lista1 = new CheckList { IdCheckList = Guid.NewGuid(), Title = "A Lista 123" };
            CheckList lista2 = new CheckList { IdCheckList = Guid.NewGuid(), Title = "Fagner Sample" };

            
            GroupCheckList grpInfoVeiculo = new GroupCheckList
            {
                IdGroupCheckList = new Guid("14109463-ecdb-48a2-a67f-a888d6548e5c"),
                IdCheckList = lista1.IdCheckList,
                Titulo = "Informações do veiculo"
            };

            List<CheckListItemAlternativa> estoque = new List<CheckListItemAlternativa>()
            {
                new CheckListItemAlternativa{ IdCheckListItemAlternativa = new Guid("7e2e950a-5b46-4d99-a857-3fc572b06f00"), Texto = "Volvo"},
                new CheckListItemAlternativa{ IdCheckListItemAlternativa = new Guid("a9b69036-9df8-4464-9763-04f2f2f2c46c"), Texto = "Estoque 1"},
                new CheckListItemAlternativa{ IdCheckListItemAlternativa = new Guid("699b014d-2252-416b-8e60-d4f4f8b10005"), Texto = "Estoque 2"}
            };

            List<CheckListItemAlternativa> grpEconomico = new List<CheckListItemAlternativa>()
            {
                new CheckListItemAlternativa{ IdCheckListItemAlternativa = new Guid("e4d60336-9e2d-407c-a673-382a66acb086"), Texto = "Nordica"},
                new CheckListItemAlternativa{ IdCheckListItemAlternativa = new Guid("b6eb8203-1631-409e-8869-b7b8d13f7b86"), Texto = "Grupo Economico 1"},
                new CheckListItemAlternativa{ IdCheckListItemAlternativa = new Guid("b4325b8a-22be-4272-ac95-748786e2e0d7"), Texto = "Grupo Economico 2"}
            };

            List<CheckListItemAlternativa> Casas = new List<CheckListItemAlternativa>()
            {
                new CheckListItemAlternativa{ IdCheckListItemAlternativa = new Guid("23fb289a-926f-4c51-b944-f63aaf6bc454"), Texto = "Curitiba"},
                new CheckListItemAlternativa{ IdCheckListItemAlternativa = new Guid("3afc9a94-2ca1-4639-8c11-908ce0c5d524"), Texto = "casa 1"},
                new CheckListItemAlternativa{ IdCheckListItemAlternativa = new Guid("3f4d382a-2ef2-4498-a9f1-6e3a675df3e4"), Texto = "casa 2"}
            };

            List<CheckListItem> itensInfoVeiculo = new List<CheckListItem>
            {
                new CheckListItem{ IdGroupCheckList = grpInfoVeiculo.IdGroupCheckList, Description = "Data", IdCheckListItem= new Guid("a795ca35-efc6-4e95-b51b-2931f1d3f068"),AnswerType = enAnswerType.Date, Options = null },
                new CheckListItem{ IdGroupCheckList = grpInfoVeiculo.IdGroupCheckList, Description = "Estoque", IdCheckListItem= new Guid("fe42caa1-fba7-4fe2-a533-539bc002315a"),AnswerType = enAnswerType.Combo, Options = estoque },
                new CheckListItem{ IdGroupCheckList = grpInfoVeiculo.IdGroupCheckList, Description = "Grupo econômico", IdCheckListItem= new Guid("de4479a5-58cb-4c15-9d3e-10c1b6798171"),AnswerType = enAnswerType.Combo, Options = grpEconomico},
                new CheckListItem{ IdGroupCheckList = grpInfoVeiculo.IdGroupCheckList, Description = "Casa", IdCheckListItem= new Guid("b8c0d463-db22-44a8-bc5e-6b893575327e"),AnswerType = enAnswerType.Combo, Options = Casas},
                new CheckListItem{ IdGroupCheckList = grpInfoVeiculo.IdGroupCheckList, Description = "Proprietário", IdCheckListItem= new Guid("ebfe936a-5c32-4397-a65a-f1980fd5911c"),AnswerType = enAnswerType.Text, Options = null},
                new CheckListItem{ IdGroupCheckList = grpInfoVeiculo.IdGroupCheckList, Description = "Marca", IdCheckListItem= new Guid("c7c32cf5-e5cb-4d1f-a705-0ba115786506"),AnswerType = enAnswerType.Text, Options = null },
                new CheckListItem{ IdGroupCheckList = grpInfoVeiculo.IdGroupCheckList, Description = "Modelo", IdCheckListItem= new Guid("2eff0361-1a88-424d-a9c2-dca1c385d0a8"),AnswerType = enAnswerType.Text, Options = null },
                new CheckListItem{ IdGroupCheckList = grpInfoVeiculo.IdGroupCheckList, Description = "Ano/Modelo", IdCheckListItem= new Guid("23e1efdb-cee3-410c-ba41-23fc3379c0cd"),AnswerType = enAnswerType.Text, Options = null },
                new CheckListItem{ IdGroupCheckList = grpInfoVeiculo.IdGroupCheckList, Description = "N° Chassi", IdCheckListItem= new Guid("a2b12ccc-e4dd-4a68-967a-610c9f88e7a3"),AnswerType = enAnswerType.Text, Options = null},
                new CheckListItem{ IdGroupCheckList = grpInfoVeiculo.IdGroupCheckList, Description = "Placa", IdCheckListItem= new Guid("832d0e62-87a0-47a1-8b56-d0d7fbfcef08"),AnswerType = enAnswerType.Text, Options = null},
                new CheckListItem{ IdGroupCheckList = grpInfoVeiculo.IdGroupCheckList, Description = "Renavam", IdCheckListItem= new Guid("bdcc6260-ad91-4cd5-ab38-0a5fdc7bbd1a"),AnswerType = enAnswerType.Text, Options = null },
                new CheckListItem{ IdGroupCheckList = grpInfoVeiculo.IdGroupCheckList, Description = "Cor", IdCheckListItem= new Guid("d883f0ee-a2e4-4830-bf27-bed5467be892"),AnswerType = enAnswerType.Text, Options = null }
            };

            grpInfoVeiculo.Itens = itensInfoVeiculo;
            lista1.Groups = new List<GroupCheckList> { grpInfoVeiculo };


            return new List<CheckList>()
            {
                lista1,
                lista2
            };
        }
    }
}
