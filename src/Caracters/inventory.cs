using items;
namespace Characters.Inventory
{
    public class InventarioSlots
    {
        private readonly Items?[] slots;

        public InventarioSlots(int numeroSlots)
        {
            slots = new Items?[numeroSlots]; // todos null por padrão
        }

        public bool Adicionar(Items item, int slot)
        {
            if (slot < 0 || slot >= slots.Length)
            {
                Console.WriteLine("Slot inválido!");
                return false;
            }
            if (slots[slot] != null)
            {
                Console.WriteLine($"Slot {slot} já ocupado!");
                return false;
            }
            slots[slot] = item;
            return true;
        }

        public bool AdicionarPrimeiroLivre(Items item)
        {
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

        public void Listar()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                string conteudo = slots[i] == null ? "(vazio)" : slots[i]!.ItemName;
                Console.WriteLine($"[{i}] {conteudo}");
            }
        }
    }
}