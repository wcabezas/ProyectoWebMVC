<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer" :clipped="$vuetify.breakpoint.lgAndUp" app>
      <v-list dense>
        <template>
          <v-list-item :to="{name:''}">
            <v-list-item-action>
              <v-icon color="grey">home</v-icon>
            </v-list-item-action>
            <v-list-item-title>Inicio</v-list-item-title>
          </v-list-item>
        </template>
        <template>
          <v-list-group>
            <template v-slot:activator>
              <v-list-item-title>Almacén</v-list-item-title>
            </template>
            <template>
              <v-list-item :to="{name:'categorias'}">
                <v-list-item-action>
                  <v-icon color="grey">category</v-icon>
                </v-list-item-action>
                <v-list-item-title>Categorías</v-list-item-title>
              </v-list-item>
            </template>
            <template>
              <v-list-item :to="{name:'articulos'}">
                <v-list-item-action>
                  <v-icon color="grey">article</v-icon>
                </v-list-item-action>
                <v-list-item-title>Artículos</v-list-item-title>
              </v-list-item>
            </template>
          </v-list-group>
          <v-list-group>
            <template v-slot:activator>
              <v-list-item-title>Compras</v-list-item-title>
            </template>
            <template>
              <v-list-item :to="{name:''}">
                <v-list-item-action>
                  <v-icon color="grey">attach_money</v-icon>
                </v-list-item-action>
                <v-list-item-title>Ingresos</v-list-item-title>
              </v-list-item>
            </template>
            <template>
              <v-list-item :to="{name:''}">
                <v-list-item-action>
                  <v-icon color="grey">group_work</v-icon>
                </v-list-item-action>
                <v-list-item-title>Proveedores</v-list-item-title>
              </v-list-item>
            </template>
          </v-list-group>
          <v-list-group>
            <template v-slot:activator>
              <v-list-item-title>Finanzas</v-list-item-title>
            </template>
            <template>
              <v-list-item :to="{name:''}">
                <v-list-item-action>
                  <v-icon color="grey">receipt</v-icon>
                </v-list-item-action>
                <v-list-item-title>Ventas</v-list-item-title>
              </v-list-item>
            </template>
            <template>
              <v-list-item :to="{name:''}">
                <v-list-item-action>
                  <v-icon color="grey">people</v-icon>
                </v-list-item-action>
                <v-list-item-title>Clientes</v-list-item-title>
              </v-list-item>
            </template>
          </v-list-group>
          <v-list-group>
            <template v-slot:activator>
              <v-list-item-title>Accesos</v-list-item-title>
            </template>
            <template>
              <v-list-item :to="{name:''}">
                <v-list-item-action>
                  <v-icon color="grey">engineering</v-icon>
                </v-list-item-action>
                <v-list-item-title>Roles</v-list-item-title>
              </v-list-item>
            </template>
            <template>
              <v-list-item :to="{name:''}">
                <v-list-item-action>
                  <v-icon color="grey">person</v-icon>
                </v-list-item-action>
                <v-list-item-title>Usuarios</v-list-item-title>
              </v-list-item>
            </template>
          </v-list-group>
                   <v-list-group>
            <template v-slot:activator>
              <v-list-item-title>Consultas</v-list-item-title>
            </template>
            <template>
              <v-list-item :to="{name:''}">
                <v-list-item-action>
                  <v-icon color="grey">insert_chart</v-icon>
                </v-list-item-action>
                <v-list-item-title>Consultas Compras</v-list-item-title>
              </v-list-item>
            </template>
            <template>
              <v-list-item :to="{name:''}">
                <v-list-item-action>
                  <v-icon color="grey">insert_chart</v-icon>
                </v-list-item-action>
                <v-list-item-title>Consultas Ventas</v-list-item-title>
              </v-list-item>
            </template>
            
          </v-list-group>
        </template>
      </v-list>
    </v-navigation-drawer>

    <v-app-bar :clipped-left="$vuetify.breakpoint.lgAndUp" app color="blue darken-3" dark>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title style="width: 300px" class="ml-0 pl-4">
        <span class="hidden-sm-and-down">Sistema</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn icon>
        <v-icon>apps</v-icon>
      </v-btn>
    </v-app-bar>
    <v-main>
      <v-container class="fill-height" fluid>
        <v-slide-y-transition mode="out-in">
          <router-view />
        </v-slide-y-transition>
      </v-container>
    </v-main>

    <v-footer padless>
      <v-col class="text-right" cols="12">
        {{ new Date().getFullYear() }} —
        <strong>Contacto: Willian.cabezasroa@gmail.com</strong>
      </v-col>
    </v-footer>
  </v-app>
</template>

<script>
import HelloWorld from "./components/HelloWorld";

export default {
  name: "App",

  components: {
    HelloWorld,
  },

  data: () => ({
    drawer: null,
  }),
};
</script>
