class Galleta:
    def _init_(self, sabor):
        self.sabor = sabor


g1 = Galleta("Chocolate")
print(f"el sabor de la galleta es {g1.sabor}")

g2 = Galleta("Chocolate")
print(f"el sabor de la galleta es {g2.sabor}")


print(g1 is g2)
print(g1.sabor == g2.sabor)
