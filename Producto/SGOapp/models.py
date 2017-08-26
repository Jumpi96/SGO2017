from django.db import models
from django.conf import settings

class Obra(models.Model):
    nombre = models.CharField(max_length=50)
    cliente = models.CharField(max_length=50)
    coeficiente = models.FloatField(default=1.0)
    fecha = models.DateField(auto_now_add=True)
    mod_usuario = models.ForeignKey(settings.AUTH_USER_MODEL, on_delete=models.SET_NULL, null=True)
    
class Receptor(models.Model):
    nombre = models.CharField(max_length=10)
    observaciones = models.CharField(max_length=100)
    
class Movimiento(models.Model):
    fecha = models.DateField(auto_now_add=True)
    usuario = models.ForeignKey(settings.AUTH_USER_MODEL, on_delete=models.SET_NULL, null=True)
    observaciones = models.CharField(max_length=100)
    cantidad = models.FloatField(default=0.0)
    receptor = models.ForeignKey(Receptor, on_delete=models.SET_NULL, null=True)

class Unidad(models.Model):
    nombre = models.CharField(max_length=10)
    descripcion = models.CharField(max_length=50, null=True)

class Rubro(models.Model):
    nombre = models.CharField(max_length=50)
    numeracion = models.CharField(max_length=10, null=True)
    
class Subrubro(models.Model):
    nombre = models.CharField(max_length=50)
    rubro = models.ForeignKey(Rubro, on_delete=models.PROTECT)

class TipoItem(models.Model):
    nombre = models.CharField(max_length=50)
    caracter = models.CharField(max_length=1)

class Item(models.Model):
    nombre = models.CharField(max_length=50)
    unidad = models.ForeignKey(Unidad, on_delete=models.PROTECT)
    subrubro = models.ForeignKey(Subrubro, on_delete=models.PROTECT)

class SubItem(models.Model):
    nombre = models.CharField(max_length=50)
    precio_unitario = models.FloatField(default=0.0)
    tipo_item = models.ForeignKey(TipoItem, on_delete=models.PROTECT)
    item = models.ForeignKey(Item, on_delete=models.PROTECT)
    unidad = models.ForeignKey(Unidad, on_delete=models.PROTECT)
    
class DetalleSubItem(models.Model):
    cantidad = models.FloatField(default=0.0)
    precio_unitario = models.FloatField(default=0.0)
    sub_item = models.ForeignKey(SubItem, on_delete=models.CASCADE)
