<template>
  <div>
    <h1>
      Users
      <b-btn v-b-modal.modalCreateUser variant="success" class="float-right">
        Create User
      </b-btn>
    </h1>

    <table v-if="pagedListOfUserListDto.totalCount" class="table">
      <thead>
        <tr>
          <th>User Name</th>
          <th>E-Mail</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in pagedListOfUserListDto.items">
          <td>{{ item.userName }}</td>
          <td>{{ item.email }}</td>
        </tr>
      </tbody>
    </table>

    <p v-else>
      <em>Loading...</em>
    </p>
    <create-user-modal-component />
  </div>
</template>

<script src="./user-list.ts"></script>
