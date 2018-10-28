<template>
  <form v-on:submit.prevent="onSubmit">
    <b-modal id="modalCreateUser" ref="modalCreateUser" title="Create User">
      <div v-for="error in errors">
        <div class="alert alert-danger" role="alert">
          {{error.value}}
        </div>
      </div>
      <div class="form-group">
        <input type="text" v-model="userName" class="form-control" placeholder="Username" required>
        <input type="text" v-model="email" class="form-control" placeholder="E-mail" required>
      </div>

      <div slot="modal-footer" class="w-100">
        <button type="submit" class="btn btn-primary float-right">
          Create
        </button>
      </div>
    </b-modal>
  </form>
</template>

<script src="./create-user-modal.ts"></script>