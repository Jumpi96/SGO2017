from django.db import models
from django.conf import settings

class Obra(models.Model):
    nombre = models.CharField(max_length=50)
    cliente = models.CharField(max_length=50)
    coeficiente = models.FloatField(default=1.0)
    fecha = models.DateField(auto_now_add=True)
    
    def __str__(self):
        return self.nombre

class Unidad(models.Model):
    nombre = models.CharField(max_length=10)
    descripcion = models.CharField(max_length=50, null=True)
    
    def __str__(self):
        return self.nombre

class Rubro(models.Model):
    nombre = models.CharField(max_length=50)
    numeracion = models.CharField(max_length=10, null=True)
    
    def __str__(self):
        return self.nombre
    
class Subrubro(models.Model):
    nombre = models.CharField(max_length=50)
    rubro = models.ForeignKey(Rubro, on_delete=models.PROTECT)
    
    def __str__(self):
        return self.nombre

class TipoItem(models.Model):
    nombre = models.CharField(max_length=50)
    caracter = models.CharField(max_length=1)
    
    def __str__(self):
        return self.nombre

class Item(models.Model):
    nombre = models.CharField(max_length=50)
    unidad = models.ForeignKey(Unidad, on_delete=models.PROTECT)
    subrubro = models.ForeignKey(Subrubro, on_delete=models.PROTECT)
    
    def __str__(self):
        return self.nombre

class SubItem(models.Model):
    nombre = models.CharField(max_length=50)
    precio_unitario = models.FloatField(default=0.0)
    tipo_item = models.ForeignKey(TipoItem, on_delete=models.PROTECT)
    item = models.ForeignKey(Item, on_delete=models.PROTECT)
    unidad = models.ForeignKey(Unidad, on_delete=models.PROTECT)
    
    def __str__(self):
        return self.nombre

class DetalleSubItem(models.Model):
    cantidad = models.FloatField(default=0.0)
    precio_unitario = models.FloatField(default=0.0)
    sub_item = models.ForeignKey(SubItem, on_delete=models.PROTECT)
    obra = models.ForeignKey(Obra, on_delete=models.PROTECT)
    
    def __str__(self):
        return self.sub_item.nombre + " - " + self.obra.nombre

class Receptor(models.Model):
    nombre = models.CharField(max_length=10)
    observaciones = models.CharField(max_length=100)
    
    def __str__(self):
        return self.nombre
    
class Movimiento(models.Model):
    fecha = models.DateField(auto_now_add=True)
    observaciones = models.CharField(max_length=100)
    cantidad = models.FloatField(default=0.0)
    receptor = models.ForeignKey(Receptor, on_delete=models.SET_NULL, null=True)
    detalle_sub_item = models.ForeignKey(DetalleSubItem, on_delete=models.PROTECT)

    def __str__(self):
        return str(self.detalle_sub_item) + " - " + self.fecha