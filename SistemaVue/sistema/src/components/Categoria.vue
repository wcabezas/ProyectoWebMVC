<template>
  <v-layout align-start>
    <v-flex>
      <v-data-table
        :headers="headers"
        :items="_categorias"
        :search="search"
        class="elevation-1"
      >
        <template v-slot:[`item.opciones`]="{ item }">
          <v-icon small class="mr-2" @click="editItem(item)">edit</v-icon>
          <template v-if="item.condicion == 'Activo'">
            <v-icon small @click="activarDesactivarMostrar(2,item)"
              >block</v-icon
            >
          </template>
          <template v-else>
            <v-icon small @click="activarDesactivarMostrar(1,item)"
              >check</v-icon
            >
          </template>
        </template>
        <template v-slot:[`item.condicion`]="{ item }">
          <h4 :class="getColor(item.condicion)" dark>
            {{ item.condicion }}
          </h4>
        </template>
        <template v-slot:top>
          <v-toolbar flat>
            <v-toolbar-title>Categorías</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>

            <v-text-field
              solo
              label="Búsqueda"
              class="text-xs-center"
              v-model="search"
              prepend-inner-icon="search"
              single-line
              hide-details
            ></v-text-field>
            <v-spacer></v-spacer>
            <v-dialog v-model="dialog" max-width="500px">
              <template v-slot:activator="{ on, attrs }">
                <v-btn
                  color="primary"
                  dark
                  class="mb-2"
                  v-bind="attrs"
                  v-on="on"
                  >Crear</v-btn
                >
              </template>
              <v-card>
                <v-card-title>
                  <span class="headline">{{ formTitle }}</span>
                </v-card-title>

                <v-card-text>
                  <v-container>
                    <v-row>
                      <v-flex xs12 sm12 md12>
                        <v-text-field
                          v-model="editedItem.nombre"
                          label="Nombre"
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm12 md12>
                        <v-text-field
                          v-model="editedItem.descripcion"
                          label="Descripción"
                        ></v-text-field>
                      </v-flex>

                      <v-flex
                        class="red--text"
                        v-for="v in validaMensaje"
                        :key="v"
                        v-text="v"
                      ></v-flex>
                    </v-row>
                  </v-container>
                </v-card-text>

                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darken-1" text @click="close"
                    >Cancelar</v-btn
                  >
                  <v-btn color="blue darken-1" text @click="guardar"
                    >Guardar</v-btn
                  >
                </v-card-actions>
              </v-card>
            </v-dialog>
            <v-dialog v-model="adModal" max-width="290px">
              <v-card>
                <v-card-title class="headline" v-if="adAccion == '1'">
                  ¿Activar Item?
                </v-card-title>
                <v-card-title class="headline" v-if="adAccion == '2'">
                  ¿Desactivar Item?
                </v-card-title>
                <v-card-text>
                  Estàs a punto de
                  <span v-if="adAccion == '1'">Activar</span>
                  <span v-if="adAccion == '2'">Desactivar</span>
                  el ìtem {{ adNombre }}
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn text color='error' @click="activarDesactivarCerrar">Cancelar</v-btn>
                  <v-btn v-if="adAccion==1" text color='primary' @click="activar">Activar</v-btn>
                  <v-btn v-if="adAccion==2" text color='primary' @click="desactivar">Desactivar</v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>
          </v-toolbar>
        </template>
        <template v-slot:no-data>
          <v-btn color="primary" @click="listar">refresh</v-btn>
        </template>
      </v-data-table>
    </v-flex>
  </v-layout>
</template>

<script>
import axios from "axios";
export default {
  data: () => ({
    categorias: [],
    dialog: false,
    headers: [
      { text: "Opciones", value: "opciones", sortable: false },
      { text: "Nombre", value: "nombre", sortable: true },
      { text: "Descripción", value: "descripcion", sortable: false },
      { text: "Estado", value: "condicion", sortable: false, color: "red" },
    ],
    search: "",
    editedIndex: -1,
    editedItem: {
      id: "",
      nombre: "",
      descripcion: "",
    },
    valida: 0,
    validaMensaje: [],
    adModal: 0,
    adAccion: 0,
    adNombre: "",
    adId: "",
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1
        ? "Nueva categoría"
        : "Actualizar categoría";
    },

    _categorias() {
      return this.categorias.map((categoria) => {
        if (categoria.condicion) {
          categoria.condicion = "Activo";
        } else {
          categoria.condicion = "Inactivo";
        }
        return categoria;
      });
    },
  },

  watch: {
    dialog(val) {
      val || this.close();
    },
  },

  created() {
    this.listar();
  },

  methods: {
    getColor(condicion) {
      if (condicion === "Activo") return "green--text text--light-1";
      else return "red--text text--light-1";
    },

    activarDesactivarMostrar(accion, item) {
      this.adModal = 1;
      this.adNombre = item.nombre;
      this.adId = item.idcategoria;
      if (accion == 1) {
        this.adAccion = 1;
      } else if (accion == 2) {
        this.adAccion = 2;
      } else {
        this.adModal = 0;
      }
    },

    listar() {
      let me = this;
      axios
        .get("api/Categorias/Listar")
        .then((response) => {
          me.categorias = response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },

    isEnabled(slot) {
      return this.enabled === slot;
    },

    editItem(item) {
      this.editedItem.id = item.idcategoria;
      this.editedItem.nombre = item.nombre;
      this.editedItem.descripcion = item.descripcion;
      this.editedIndex = 1;
      this.dialog = true;
    },

    deleteItem(item) {
      const index = this.desserts.indexOf(item);
      confirm("Are you sure you want to delete this item?") &&
        this.desserts.splice(index, 1);
    },

    close() {
      this.dialog = false;
      this.limpiar();
    },

    limpiar() {
      this.editedItem.id = "";
      this.editedItem.nombre = "";
      this.editedItem.descripcion = "";
      this.editedIndex = -1;
    },

    guardar() {
      if (this.validar()) {
        return;
      }
      if (this.editedIndex > -1) {
        let me = this;
        axios
          .put("api/Categorias/Actualizar", {
            idcategoria: me.editedItem.id,
            nombre: me.editedItem.nombre,
            descripcion: me.editedItem.descripcion,
          })
          .then((item) => {
            me.close();
            me.listar();
            me.limpiar();
          })
          .catch((error) => {
            console.log(error);
          });
      } else {
        let me = this;
        axios
          .post("api/Categorias/Crear", {
            nombre: me.editedItem.nombre,
            descripcion: me.editedItem.descripcion,
          })
          .then((item) => {
            me.close();
            me.listar();
            me.limpiar();
          })
          .catch((error) => {
            console.log(error);
          });
      }
    },

    validar() {
      const item = this.editedItem;
      this.valida = 0;
      this.validaMensaje = [];
      if (item.nombre.length < 3 || item.nombre.length > 50) {
        this.validaMensaje.push(
          "El nombre debe tener mas de 3 caracteres y menos de 50 caracteres"
        );
        console.log(this.validaMensaje);
      }
      if (this.validaMensaje.length) {
        this.valida = 1;
      }
      return this.valida;
    },

    activar(){
      let me = this;
        axios
          .put("api/Categorias/Activar/"+this.adId, {})
          .then((item) => {
            me.adModal=0;
            me.adAccion=0;
            me.adNombre='';
            me.adId='';
            me.listar();
          })
          .catch((error) => {
            console.log(error);
          });
    },

    desactivar(){
      let me = this;
        axios
          .put("api/Categorias/Desactivar/"+this.adId, {})
          .then((item) => {
            me.adModal=0;
            me.adAccion=0;
            me.adNombre='';
            me.adId='';
            me.listar();
          })
          .catch((error) => {
            console.log(error);
          });
    },

    activarDesactivarCerrar(){
      this.adModal=0;
    }
  },
};
</script>