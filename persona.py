class Persona:
    def _init_(self, nombre, edad):
        self.nombre = nombre
        self.edad = edad

    def saludar(self):
        return f"Hola, soy {self.nombre} y tengo {self.edad} años"


p = Persona("Anyel Torregroza", 18)
print(p.saludar())
