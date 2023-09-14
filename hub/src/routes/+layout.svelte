<script lang="ts">
    import "$lib/global.css";
</script>

<div class="w-screen mx-auto">
	<div class="container">
		<div class="navbar w-screen shadow-lg bg-neutral text-neutral-content">
			<div class="navbar-start">
			<a class="btn btn-ghost normal-case text-xl font-bold" href="/">Future Frictions</a>
			<ul class="menu menu-horizontal px-1">
				<li><a href="/scenarios">Browse</a></li>
			</ul>
			</div>
		</div>	
	</div>

	<div class="container w-screen mx-auto pt-5">
		<slot />
	</div>

</div>
