using items;
using System;

namespace Characters.Inventory
{
    public class InventarioSlots
    {
        private readonly Items?[] slots;

        // Limite padrão por slot (você pode tornar isso configurável depois)
        private const int MaxStackPadrao = 99;

        public InventarioSlots(int numeroSlots)
        {
            slots = new Items?[numeroSlots];
        }

        public bool Adicionar(Items item, int slot)
        {
            if (slot < 0 || slot >= slots.Length)
            {
                Console.WriteLine("Slot inválido!");
                return false;
            }

            // Slot vazio → coloca direto
            if (slots[slot] == null)
            {
                slots[slot] = item;
                return true;
            }

            // Slot ocupado → verifica se é o MESMO item (mesmo nome)
            if (slots[slot]!.ItemName == item.ItemName)
            {
                int espacoDisponivel = MaxStackPadrao - slots[slot]!.Quantity;

                if (espacoDisponivel <= 0)
                {
                    Console.WriteLine($"Slot {slot} já está cheio ({MaxStackPadrao})!");
                    return false;
                }

                int quantidadeAdicionar = Math.Min(espacoDisponivel, item.Quantity);
                slots[slot]!.Quantity += quantidadeAdicionar;
                item.Quantity -= quantidadeAdicionar;

                Console.WriteLine($"Empilhado no slot {slot}. Quantidade atual: {slots[slot]!.Quantity}");

                // Sobrou item? Tenta em outro slot
                if (item.Quantity > 0)
                    return AdicionarPrimeiroLivre(item);

                return true;
            }

            Console.WriteLine($"Slot {slot} já ocupado por outro item!");
            return false;
        }

        public bool AdicionarPrimeiroLivre(Items item)
        {
            // 1ª passada: tenta empilhar em slots com o mesmo item
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] != null &&
                    slots[i]!.ItemName == item.ItemName &&
                    slots[i]!.Quantity < MaxStackPadrao)
                {
                    int espaco = MaxStackPadrao - slots[i]!.Quantity;
                    int qtd = Math.Min(espaco, item.Quantity);

                    slots[i]!.Quantity += qtd;
                    item.Quantity -= qtd;

                    Console.WriteLine($"Empilhado no slot {i}. Quantidade atual: {slots[i]!.Quantity}");

                    if (item.Quantity <= 0)
                        return true;
                }
            }

            // 2ª passada: coloca em um slot vazio
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] == null)
                {
                    slots[i] = item;
                    return true;
                }
            }

            Console.WriteLine("Sem slots livres!");
            return false;
        }

        public Items? Obter(int slot) => slots[slot];

        public void Remover(int slot) => slots[slot] = null;

        // Remove X unidades de um item em um slot
        public bool RemoverQuantidade(int slot, int quantidade)
        {
            if (slot < 0 || slot >= slots.Length || slots[slot] == null)
                return false;

            if (quantidade >= slots[slot]!.Quantity)
            {
                slots[slot] = null;
                return true;
            }

            slots[slot]!.Quantity -= quantidade;
            return true;
        }

        public void Listar()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                string conteudo = slots[i] == null
                    ? "(vazio)"
                    : $"{slots[i]!.ItemName} x{slots[i]!.Quantity}";
                Console.WriteLine($"[{i}] {conteudo}");
            }
        }
    }
}